namespace Game.Utils.Movement
{
    public readonly struct ChunkCenterParameters
    {
        public readonly bool IsMoveAlongZ;
        public readonly float CenterValue;
        
        public ChunkCenterParameters(bool isMoveAlongZ, float centerValue)
        {
            IsMoveAlongZ = isMoveAlongZ;
            CenterValue = centerValue;
        }
    }
}