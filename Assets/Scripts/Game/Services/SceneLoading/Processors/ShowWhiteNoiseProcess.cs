using System;
using Game.Services.Render;
using Game.Utils.Render;
using PdUtils.SceneLoadingProcessor.Impls;

namespace Game.Services.SceneLoading.Processors
{
    public sealed class ShowWhiteNoiseProcess : Process
    {
        private readonly IGlitchService _glitchService;
        private readonly IWhiteNoiseService _whiteNoiseService;

        public ShowWhiteNoiseProcess(
            IGlitchService glitchService,
            IWhiteNoiseService whiteNoiseService
        )
        {
            _glitchService = glitchService;
            _whiteNoiseService = whiteNoiseService;
        }

        public override void Do(Action onComplete)
        {
            _glitchService.StopGlitchesImmediately(EPipelineAssetType.UiCamera);
            _whiteNoiseService.StartWhiteNoiseImmediately(EPipelineAssetType.UiCamera);
            onComplete();
        }
    }
}