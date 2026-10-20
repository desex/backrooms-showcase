using System;
using Game.Services.Render;
using Game.Utils.Render;
using PdUtils.SceneLoadingProcessor.Impls;

namespace Game.Services.SceneLoading.Processors
{
    public sealed class ShowGlitchProcess : Process
    {
        private readonly IGlitchService _glitchService;
        private readonly IWhiteNoiseService _whiteNoiseService;

        public ShowGlitchProcess(
            IGlitchService glitchService,
            IWhiteNoiseService whiteNoiseService)
        {
            _glitchService = glitchService;
            _whiteNoiseService = whiteNoiseService;
        }

        public override void Do(Action onComplete)
        {
            _glitchService.StartGlitchesImmediately(EPipelineAssetType.UiCamera);
            _whiteNoiseService.StopWhiteNoiseImmediately(EPipelineAssetType.UiCamera);
            onComplete();
        }
    }
}