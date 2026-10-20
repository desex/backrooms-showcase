namespace Ecs.Utils.ObstaclePass
{
    public partial struct PassObstacleMovePoint
    {
        public enum EActionType
        {
            None = 0,
            Animation = 1,
            VFX = 2,
            EnableAnimationGameObject = 3,
            PlayHitOn = 4,
            OneObjectAnimation = 5,
        }
    }
}