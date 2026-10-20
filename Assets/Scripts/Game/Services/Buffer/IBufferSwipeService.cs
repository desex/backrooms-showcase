namespace Game.Services.Buffer
{
    public interface IBufferSwipeService
    {
        void BufferSwipe(GameEntity player, float swipeDirection, float swipeThreshold);
        void CallBufferSwipe(GameEntity player);
    }
}