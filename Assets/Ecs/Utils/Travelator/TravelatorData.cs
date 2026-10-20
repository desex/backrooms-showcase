namespace Ecs.Utils.Travelator
{
    public readonly struct TravelatorData
    {
        public readonly float PlayerSpeedAdjustmentFactor;
        public readonly float StartForwardSpeed;
        
        public TravelatorData(float playerSpeedAdjustmentFactor, float startForwardSpeed)
        {
            PlayerSpeedAdjustmentFactor = playerSpeedAdjustmentFactor;
            StartForwardSpeed = startForwardSpeed;
        }
    }
}