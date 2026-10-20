using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Db.Graphics;
using Game.Providers;
using Game.Utils;
using Game.Utils.Performance;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Game.Services.Performance.Impl
{
    public sealed class AdaptivePerformanceService : IAdaptivePerformanceService
    {
        private static readonly string SSAO_FEATURE_NAME = "HBAO";
        private readonly IAdaptivePerformanceSettingsBase _adaptivePerformanceSettings;
        private readonly Queue<GraphicsDowngrade> _downgradesQueue;
        private readonly FrameTiming[] _frameTimings = new FrameTiming[9];
        private readonly IRendererFeatureProvider _rendererFeatureProvider;

        private readonly IUrpSettingsBase _urpSettings;

        private FieldInfo _additionalLightsRenderingModeFieldInfo;
        private bool _isEnabled;

        public AdaptivePerformanceService(
            IUrpSettingsBase urpSettings,
            IAdaptivePerformanceSettingsBase adaptivePerformanceSettings,
            IRendererFeatureProvider rendererFeatureProvider
        )
        {
            _urpSettings = urpSettings;
            _adaptivePerformanceSettings = adaptivePerformanceSettings;
            _rendererFeatureProvider = rendererFeatureProvider;

            SetupSettingsFields();
            _downgradesQueue = new Queue<GraphicsDowngrade>(adaptivePerformanceSettings.DownGradesOrder);
            _isEnabled = adaptivePerformanceSettings.IsAdaptivePerformanceEnabled;
        }

        public bool IsGpuTimeLowerThanNeeded => CheckIfNeedToDecreaseGraphicsSettings();

        public bool TryDecreaseGraphicsSettings()
        {
            if (!_isEnabled)
                return false;

            if (_downgradesQueue.Count == 0)
            {
                _isEnabled = false;

                return false;
            }

            var currentDowngrade = _downgradesQueue.Peek();

            var canBeChangedNextTime = currentDowngrade.Action switch
            {
                EAdaptivePerformanceAction.Increase => SmoothChangeValue(
                    currentDowngrade,
                    x => x + currentDowngrade.Step,
                    currentDowngrade.IncreaseTarget,
                    x => x > currentDowngrade.IncreaseTarget
                ),
                EAdaptivePerformanceAction.Decrease => SmoothChangeValue(
                    currentDowngrade,
                    x => x - currentDowngrade.Step,
                    currentDowngrade.DecreaseTarget,
                    x => x < currentDowngrade.DecreaseTarget
                ),
                EAdaptivePerformanceAction.ToggleEnable => DiscreteChangeValue(currentDowngrade, true),
                EAdaptivePerformanceAction.ToggleDisable => DiscreteChangeValue(currentDowngrade, false),
                _ => throw new InvalidDataException(),
            };

            if (!canBeChangedNextTime)
                _downgradesQueue.Dequeue();

            return true;
        }

        private void SetupSettingsFields()
        {
            var pipelineAssetType = typeof(UniversalRenderPipelineAsset);
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;

            _additionalLightsRenderingModeFieldInfo =
                pipelineAssetType.GetField("m_AdditionalLightsRenderingMode", flags);
        }

        private bool CheckIfNeedToDecreaseGraphicsSettings()
        {
            if (!_isEnabled)
                return false;

            FrameTimingManager.CaptureFrameTimings();

            var returnCount = FrameTimingManager.GetLatestTimings((uint)_frameTimings.Length, _frameTimings);

            if (returnCount > 0)
            {
                var medianGpuFrequency = GetMedianGpuFrequency(_frameTimings);
                var targetFps = _adaptivePerformanceSettings.FpsTarget;

                if (medianGpuFrequency < targetFps)
                    return true;
            }

            return false;
        }

        private static uint GetMedianGpuFrequency(IList<FrameTiming> timings)
        {
            Span<double> gpuTimes = stackalloc double[timings.Count];

            for (var i = 0; i < timings.Count; i++)
                gpuTimes[i] = timings[i].gpuFrameTime;

            var medianGpuTime = gpuTimes.GetMedianValue();

            if (medianGpuTime == 0d)
                return uint.MaxValue;

            var medianGpuFrequency = (uint)(1000 / medianGpuTime);

            return medianGpuFrequency;
        }

        private bool SmoothChangeValue(
            GraphicsDowngrade currentDowngrade,
            Func<float, float> stepChange,
            float limit,
            Func<float, bool> checkLimit
        )
        {
            if (currentDowngrade.SmoothDowngradeType == EGraphicsSmoothDowngradeType.RenderScale)
                return ChangeRenderScale(stepChange, limit, checkLimit);

            throw new InvalidDataException();
        }

        private bool ChangeRenderScale(Func<float, float> stepChange, float limit, Func<float, bool> checkLimit)
        {
            var value = _urpSettings.UniversalRenderPipelineAsset.renderScale;
            value = stepChange.Invoke(value);

            var canBeChangedNextTime = true;

            if (checkLimit.Invoke(value))
            {
                value = limit;
                canBeChangedNextTime = false;
            }

            _urpSettings.UniversalRenderPipelineAsset.renderScale = value;

            return canBeChangedNextTime;
        }

        private bool DiscreteChangeValue(GraphicsDowngrade currentDowngrade, bool isEnabled)
        {
            return currentDowngrade.DiscreteDowngradeType switch
            {
                EGraphicsDiscreteDowngradeType.ScreenSpaceAmbientOcclusion => ChangeSsao(isEnabled),
                EGraphicsDiscreteDowngradeType.AdditionalLights => ChangeAdditionalLights(isEnabled),
                _ => throw new InvalidDataException(),
            };
        }

        private bool ChangeSsao(bool isEnabled)
        {
            var ssaoFeature = _rendererFeatureProvider.FindRendererFeature(SSAO_FEATURE_NAME);

            ssaoFeature.SetActive(isEnabled);

            return false;
        }

        private bool ChangeAdditionalLights(bool isEnabled)
        {
            var pipelineAsset = _urpSettings.UniversalRenderPipelineAsset;

            var newValue = isEnabled switch
            {
                true => LightRenderingMode.PerPixel,
                false => LightRenderingMode.Disabled,
            };

            _additionalLightsRenderingModeFieldInfo.SetValue(pipelineAsset, newValue);

            return false;
        }
    }
}