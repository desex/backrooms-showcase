using Ecs.Managers;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Player
{
    [Game]
    [Event(EventTarget.Self)]
    public class CurrentChunkIdComponent : IComponent
    {
        public Uid Value;
    }
}