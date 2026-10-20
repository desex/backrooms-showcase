namespace Game.Utils.Ghost
{
    public readonly struct GhostChancesData
    {
        public readonly float JumpFailChance;
        public readonly float ComeBackFailChance;
        public readonly float TurnFailChance;
        public readonly float ObstacleFailChance;
        public readonly float ObstacleFastZoneChance;

        public GhostChancesData(
            float jumpFailChance, 
            float comeBackFailChance, 
            float turnFailChance, 
            float obstacleFailChance, 
            float obstacleFastZoneChance
        )
        {
            JumpFailChance = jumpFailChance;
            ComeBackFailChance = comeBackFailChance;
            TurnFailChance = turnFailChance;
            ObstacleFailChance = obstacleFailChance;
            ObstacleFastZoneChance = obstacleFastZoneChance;
        }
    }
}