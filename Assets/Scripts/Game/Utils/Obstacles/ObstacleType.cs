using System;

namespace Game.Utils.Obstacles
{
    [Serializable]
    public partial struct ObstacleType
    {
        public static ObstacleType None => 0;
        public static ObstacleType VerticalColumn => (ObstacleType)1 << 0;
        public static ObstacleType HorizontalFloorColumn => (ObstacleType)1 << 1;
        public static ObstacleType HorizontalHeadLevelColumn => (ObstacleType)1 << 2;
        public static ObstacleType VerticalPipe => (ObstacleType)1 << 3;
        public static ObstacleType HorizontalFloorPipe => (ObstacleType)1 << 4;
    }
}