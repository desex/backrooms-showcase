using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Providers;
using Game.Utils.Render;
using Game.Utils.UniTaskExtensions;
using Project.Render.RendererFeatures.Impl;
using UnityEngine;

namespace Game.Services.Render.Impl
{
    public sealed class TvLinesService : ARendererFeatureService<TvLinesRendererFeature>, ITvLinesService
    {
        private CancellationTokenSource _cancellationTokenSource;

        public TvLinesService(
            IRendererFeatureProvider rendererFeatureProvider
        ) : base(rendererFeatureProvider, true, false) { }

        public void StartTvLinesImmediately(EPipelineAssetType pipelineAssetType)
        {
            var feature = GetRendererFeature(pipelineAssetType);
            feature.SetActive(true);
            feature.Settings.ShaderStrength = 1;
        }

        public void StartTvLinesSmoothly(EPipelineAssetType pipelineAssetType, float smoothTime)
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = _cancellationTokenSource.Token;

            var feature = GetRendererFeature(pipelineAssetType);
            feature.SetActive(true);

            SmoothlyChangeShaderStrength(
                    pipelineAssetType, 
                    smoothTime, 
                    0, 
                    1, 
                    cancellationToken, 
                    () => feature.Settings.ShaderStrength = 1
                    ).ForgetAndWrapExceptions();
        }

        public void StopTvLinesImmediately(EPipelineAssetType pipelineAssetType)
        {
            var feature = GetRendererFeature(pipelineAssetType);
            feature.Settings.ShaderStrength = 0;
            feature.SetActive(false);
        }

        public void StopTvLinesSmoothly(EPipelineAssetType pipelineAssetType, float smoothTime)
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = _cancellationTokenSource.Token;

            var feature = GetRendererFeature(pipelineAssetType);
            feature.SetActive(true);

            SmoothlyChangeShaderStrength(
                    pipelineAssetType,
                    smoothTime,
                    1,
                    0,
                    cancellationToken,
                    () =>
                    {
                        feature.Settings.ShaderStrength = 0;
                        feature.SetActive(false);
                    }
                )
                .ForgetAndWrapExceptions();
        }

        private async UniTask SmoothlyChangeShaderStrength(
            EPipelineAssetType pipelineAssetType,
            float smoothTime,
            float startValue,
            float endValue,
            CancellationToken cancellationToken,
            Action onComplete = null
        )
        {
            var currentTime = 0f;

            while (currentTime <= smoothTime)
            {
                currentTime += Time.deltaTime;

                var tParameter = Mathf.Clamp01(currentTime / smoothTime);

                var strength = Mathf.Lerp(startValue, endValue, tParameter);

                var rendererFeature = GetRendererFeature(pipelineAssetType);
                rendererFeature.Settings.ShaderStrength = strength;

                await UniTask.Yield();

                if (cancellationToken.IsCancellationRequested)
                    return;
            }

            onComplete?.Invoke();
        }
    }
}