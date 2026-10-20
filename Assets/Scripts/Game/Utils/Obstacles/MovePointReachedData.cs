using Ecs.Managers;

namespace Game.Utils.Obstacles
{
    public readonly struct MovePointReachedData
    {
        public readonly int PointIndex;
        public readonly Uid CharacterUid;

        public MovePointReachedData(
            int pointIndex, 
            Uid characterUid
        )
        {
            PointIndex = pointIndex;
            CharacterUid = characterUid;
        }
    }
}