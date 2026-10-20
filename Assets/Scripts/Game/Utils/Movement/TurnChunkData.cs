using Ecs.Managers;

namespace Game.Utils.Movement
{
    public readonly struct TurnChunkData
    {
        public readonly ETurnDirection Direction;
        public readonly Uid ChunkUid;

        public TurnChunkData(
            ETurnDirection direction, 
            Uid chunkUid
        )
        {
            Direction = direction;
            ChunkUid = chunkUid;
        }
    }
}