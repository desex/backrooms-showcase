using System;
using Core.Utils.TriggerCollision;
using Game.Utils.Movement;

namespace Game.Utils.Obstacles
{
    [Serializable]
    public class DangerZoneTrigger
    {
        public Box Trigger;
        public EObstacleTypeForAcceleration ObstacleTypeForAcceleration;
    }
}