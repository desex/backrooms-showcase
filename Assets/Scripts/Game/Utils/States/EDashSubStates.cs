namespace Game.Utils.States
{
    public enum EDashSubStates
    {
        None = 0,
        WaitFirstWaterPickUpHint = 1,
        WaitFirstWaterPickUp = 2,
        WaitPlayerNearColumns = 3,
        WaitDashThroughColumns = 4,
        WaitSecondWaterPickUp = 5,
        WaitPlayerNearSecondColumns = 6,
        WaitDashThroughSecondColumns = 7,
        End = 8,
    }
}