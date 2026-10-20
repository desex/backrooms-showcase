using Game.Utils.Buffer;
using Generated.Commands;
using JCMG.EntitasRedux.Commands;
using UnityEngine;

namespace Game.Services.Buffer.Impl
{
    public class BufferSwipeService : IBufferSwipeService
    {
        private readonly ICommandBuffer _commandBuffer;

        public BufferSwipeService(ICommandBuffer commandBuffer)
        {
            _commandBuffer = commandBuffer;
        }
        
        public void BufferSwipe(GameEntity player, float swipeDirection, float swipeThreshold)
        {
            if (!player.HasDamageReboundTime)
                return;

            if (player.HasBufferSwipe)
                player.RemoveBufferSwipe();
            
            if (swipeDirection > swipeThreshold)
                player.ReplaceBufferSwipe(ESwipeDirection.Up);
            else if (swipeDirection < -swipeThreshold)
                player.ReplaceBufferSwipe(ESwipeDirection.Down);
        }

        public void CallBufferSwipe(GameEntity player)
        {
            if (!player.HasBufferSwipe)
                return;

            if (!player.HasReadyToObstaclePass)
            {
                player.RemoveBufferSwipe();
                return;
            }

            switch (player.BufferSwipe.SwipeDirection)
            {
                case ESwipeDirection.Up:
                    _commandBuffer.Swipe(Vector2.up);
                    break;

                case ESwipeDirection.Down:
                    _commandBuffer.Swipe(Vector2.down);
                    break;
            }
            
            player.RemoveBufferSwipe();
        }
    }
}