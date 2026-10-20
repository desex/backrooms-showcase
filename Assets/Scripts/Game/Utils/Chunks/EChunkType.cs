using System;

namespace Game.Utils.Chunks
{
    //If you change something here then you must change the same here:
    //https://docs.google.com/spreadsheets/d/REDACTED/edit?gid=690196057#gid=690196057
    [Flags]
    public enum EChunkType
    {
        None = 0,
        Straight = 1 << 0,
        TurnLeft = 1 << 1,
        TurnRight = 1 << 2,
        TurnBothSides = 1 << 3,
        Down = 1 << 4,
        DiagonalRight = 1 << 6,
        Dark = 1 << 7,
        StraightColumn = 1 << 8,
        CrossColumn = 1 << 9,
        Curved = 1 << 10,
        Rotated = 1 << 11,
        TurnLeftBody = 1 << 12,
        TurnRightBody = 1 << 13,
        TurnBothSidesBody = 1 << 14,
        Transition = 1 << 15,
        TutorialTurnLeftV1 = 1 << 16,
        TutorialTurnRightV1 = 1 << 17,
        TutorialTurnRightV2 = 1 << 18,
        TutorialTurnLeftV2 = 1 << 19,
        TransitionHole = 1 << 20,
        TransitionTruck = 1 << 21,
        TransitionTrain = 1 << 22,
        Everything = int.MaxValue,
    }
}