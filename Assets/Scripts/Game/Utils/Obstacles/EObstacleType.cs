using System;

namespace Game.Utils.Obstacles
{
    //If you change something here then you must change the same here:
    //https://docs.google.com/spreadsheets/d/REDACTED/edit?gid=690196057#gid=690196057
    [Flags]
    public enum EObstacleType
    {
        None = 0,
        VerticalColumn = 1 << 0,
        HorizontalFloorColumn = 1 << 1,
        HorizontalHeadLevelColumn = 1 << 2,
        VerticalPipe = 1 << 3,
        HorizontalFloorPipe = 1 << 4,
        HorizontalHeadLevelPipe = 1 << 5,
        CurvedColumnLeftToRight = 1 << 6,
        BigScientist = 1 << 7,
        DestructibleWood = 1 << 8,
        MovableWall = 1 << 9,
        NarrowHole = 1 << 10,
        WideHole = 1 << 11,
        Railings = 1 << 12,
        BoxesWithAwning = 1 << 13,
        BreakBox = 1 << 14,
        HoleWithWallForRunning = 1 << 15,
        WallWithHole = 1 << 16,
        HandWithoutWalls = 1 << 17,
        TruckWithAutomobileRight = 1 << 18,
        Stairs = 1 << 19,
        Wall = 1 << 20,
        WallWithDownHole = 1 << 21,
        TruckWithHole = 1 << 22,
        Train = 1 << 23,
        FallingScientist = 1 << 24,
        Hand = 1 << 25,
        SmallHole = 1 << 26,
        MediumHole = 1 << 27,
        GdObstacle = 1 << 28,
        Glass = 1 << 29,
        DestructiblePlastic = 1 << 30,
        Swing = 1 << 31,
        Everything = int.MaxValue, 
    }
}