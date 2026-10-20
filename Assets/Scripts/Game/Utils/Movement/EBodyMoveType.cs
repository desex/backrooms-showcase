namespace Game.Utils.Movement
{
    public enum EBodyMoveType
    {
        None = 0,
        Run = 1,
        Jump = 2,
        InterruptFall = 3,
        Roll = 4,
        ShortSlide = 5,
        LongSlide = 6,
        MonkeyJump = 7,
        HitOnObstacle = 9,
        ControllableSlide = 10,
        ForwardJump = 11,
        JumpOn = 12,
        Climb = 13,
        WallRun = 14,
        TransitionExit = 16,
        TransitionEnter = 17,
        Reviving = 19,
        DashInHole = 20,
        ComebackInHole = 21,
        JumpInObstacle = 22,
        JumpInObstacleCut = 23,
        Swing = 24,
        RotatedBack = 25,
        Landing = 26,
    }
}