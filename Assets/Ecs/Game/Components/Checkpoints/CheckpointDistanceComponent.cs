using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Checkpoints
{
    [Game]
    [Event(EventTarget.Self)]
    public class CheckpointDistanceComponent : IComponent
    {
        public int Value;
    }
}