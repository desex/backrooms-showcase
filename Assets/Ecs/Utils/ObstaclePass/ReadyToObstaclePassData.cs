using Ecs.Managers;
using Game.Utils.Movement;

namespace Ecs.Utils.ObstaclePass
{
    public readonly struct ReadyToObstaclePassData
    {
        public readonly EObstaclePassType PassType;
        public readonly EObstacleTypeForAcceleration ObstacleTypeForAcceleration;
        public readonly bool IsDashEnabled;
        public readonly Uid Uid;

        public ReadyToObstaclePassData(
            Uid uid,
            EObstaclePassType passType,
            bool isDashEnabled,
            EObstacleTypeForAcceleration obstacleTypeForAcceleration
        )
        {
            Uid = uid;
            PassType = passType;
            IsDashEnabled = isDashEnabled;
            ObstacleTypeForAcceleration = obstacleTypeForAcceleration;
        }
    }
}