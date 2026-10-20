namespace Game.Services.Player
{
    public interface IAccelerationFactorService
    {
        void SetAccelerationFactor(float factor);
        float GetAccelerationFactor();
        void InterpolateAccelerationFactor();
    }
}