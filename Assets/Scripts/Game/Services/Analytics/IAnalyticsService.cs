using Ecs.Managers;
using Ecs.Utils;
using Game.Utils;
using Game.Utils.Lights;

namespace Game.Services.Analytics
{
    public interface IAnalyticsService
    {
        void HandleLevelStart();
        void HandleLevelRestart();
        void HandleRevive();
        void HandlePlayerDeath(EDeathReason deathReason);
        void HandleLevelFinish();
        void SetCurrentChunk(Uid chunkUid);
        void HandleObstacleHit(Uid hitEntityUid);
        void SetLightEvent(ELightEvent lightEvent);
        void HandleScreamerStart(EScreamerType screamerType);
        void HandleScreamerEnd(EScreamerType screamerType);
        void HandleLevelTransition(EGameLevelType levelType);
    }
}