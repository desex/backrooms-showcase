using Game.Utils;

namespace Game.Services.Effects
{
    public interface IUnderWaterRendererService
    {
        void SetLevelRenderer(EGameLevelType targetLevelType);
        void SetUnderWaterLevel(float waterLevel);
        void ResetUnderWaterLevel();
    }
}