using Game.Utils.Render;

namespace Game.Services.Render
{
    public interface IWhiteNoiseService
    {
        void StartWhiteNoiseImmediately(EPipelineAssetType pipelineAssetType);
        void StopWhiteNoiseImmediately(EPipelineAssetType pipelineAssetType);
    }
}