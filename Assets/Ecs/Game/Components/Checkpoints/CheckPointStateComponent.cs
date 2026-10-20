using Game.Utils.Checkpoint;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Checkpoints
{
    [Game]
    [Event(EventTarget.Self)]
    public class CheckPointStateComponent : IComponent
    {
        public ECheckpointTvState Value;
    }
}