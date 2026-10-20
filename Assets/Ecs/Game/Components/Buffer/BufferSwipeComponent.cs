using Game.Utils.Buffer;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Buffer
{
    [Game]
    public class BufferSwipeComponent : IComponent
    {
        public ESwipeDirection SwipeDirection;
    }
}