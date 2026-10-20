using System.Collections.Generic;
using Ecs.Managers;

namespace Game.Services.CheckPoints
{
    public interface ICheckpointsPool
    {
        IReadOnlyList<GameEntity> Active { get; }

        GameEntity Get();
        void Return(GameEntity checkpoint);
        bool TryReturn(Uid chunkUid);
    }
}