using Game.Utils.Render;

namespace Game.Services.Render
{
    public interface ITvLinesService
    {
        void StartTvLinesImmediately(EPipelineAssetType pipelineAssetType);
        void StartTvLinesSmoothly(EPipelineAssetType pipelineAssetType, float smoothTime);
        void StopTvLinesImmediately(EPipelineAssetType pipelineAssetType);
        void StopTvLinesSmoothly(EPipelineAssetType pipelineAssetType, float smoothTime);
    }
}