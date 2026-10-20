using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Providers;
using Game.Utils.Render;
using Game.Utils.UniTaskExtensions;
using Project.Render.RendererFeatures.Impl;
using UnityEngine;

namespace Game.Services.Render.Impl
{
    public sealed class GlitchService : ARendererFeatureService<GlitchRendererFeature>, IGlitchService
    {
        private CancellationTokenSource _cancellationTokenSource;

        public GlitchService(
            IRendererFeatureProvider rendererFeatureProvider
        ) : base(rendererFeatureProvider, true, true) { }

        public void StartGlitchesImmediately(EPipelineAssetType pipelineAssetType = EPipelineAssetType.Default)
        {
            _cancellationTokenSource?.Cancel();

            var rendererFeature = GetRendererFeature(pipelineAssetType);

            rendererFeature.Settings.UsePeriod = true;
            rendererFeature.Settings.ShaderStrength = 1;
            rendererFeature.SetActive(true);
        }

        public void StopGlitchesImmediately(EPipelineAssetType pipelineAssetType = EPipelineAssetType.Default)
        {
            _cancellationTokenSource?.Cancel();

            var rendererFeature = GetRendererFeature(pipelineAssetType);

            rendererFeature.SetActive(false);
        }

        public void StartGlitches(
            float duration,
            float fadeTime,
            EPipelineAssetType pipelineAssetType = EPipelineAssetType.Default
        )
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = _cancellationTokenSource.Token;

            var rendererFeature = GetRendererFeature(pipelineAssetType);

            rendererFeature.Settings.UsePeriod = false;
            rendererFeature.Settings.ShaderStrength = 0;
            rendererFeature.SetActive(true);

            DoGlitches(duration, fadeTime, pipelineAssetType, cancellationToken)
                .ForgetAndWrapExceptions();
        }

        private async UniTask DoGlitches(
            float duration, 
            float fadeTime, 
            EPipelineAssetType pipelineAssetType, 
            CancellationToken cancellationToken
        )
        {
            var passedTime = 0f;

            var rendererFeature = GetRendererFeature(pipelineAssetType);

            while (passedTime < fadeTime)
            {
                passedTime += Time.deltaTime;

                var tParameter = Mathf.Clamp01(passedTime / fadeTime);

                rendererFeature.Settings.ShaderStrength = Mathf.Lerp(0, 1, tParameter);

                await UniTask.Yield(cancellationToken);
            }

            var timeBeforeFade = duration - fadeTime;

            while (passedTime < timeBeforeFade)
            {
                passedTime += Time.deltaTime;

                await UniTask.Yield(cancellationToken);
            }

            while (passedTime < duration)
            {
                passedTime += Time.deltaTime;

                var tParameter = Mathf.Clamp01(passedTime - timeBeforeFade / fadeTime);

                rendererFeature.Settings.ShaderStrength = Mathf.Lerp(1, 0, tParameter);

                await UniTask.Yield(cancellationToken);
            }

            rendererFeature.SetActive(false);
        }
    }
}