using Game.Utils.Render;

namespace Game.Services.Render
{
    public interface IGlitchService
    {
        void StartGlitchesImmediately(EPipelineAssetType pipelineAssetType = EPipelineAssetType.Default);
        void StopGlitchesImmediately(EPipelineAssetType pipelineAssetType = EPipelineAssetType.Default);

        void StartGlitches(
            float duration,
            float fadeTime,
            EPipelineAssetType pipelineAssetType = EPipelineAssetType.Default
        );
    }
}