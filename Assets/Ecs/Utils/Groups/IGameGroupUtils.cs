using System;
using System.Collections.Generic;

namespace Ecs.Utils.Groups
{
    public interface IGameGroupUtils
    {
        IDisposable GetAi(out List<GameEntity> buffer, Func<GameEntity, bool> filter = null, bool nonDestroyed = true);
        IDisposable GetGhosts(out List<GameEntity> buffer, Func<GameEntity, bool> filter = null);
        IDisposable GetCharacters(out List<GameEntity> buffer, Func<GameEntity, bool> filter = null);
        IDisposable GetTimers(out List<GameEntity> buffer, Func<GameEntity, bool> filter = null);
        IDisposable GetBerserkModeTimers(out List<GameEntity> buffer, Func<GameEntity, bool> filter = null);
        IDisposable GetObstacleGroups(out List<GameEntity> buffer, Func<GameEntity, bool> filter = null);
        IDisposable GetCheckpoints(out List<GameEntity> buffer, Func<GameEntity, bool> filter = null);
        IDisposable GetCollectables(out List<GameEntity> buffer, Func<GameEntity, bool> filter = null);
        IDisposable GetChunks(out List<GameEntity> buffer, Func<GameEntity, bool> filter = null);
    }
}