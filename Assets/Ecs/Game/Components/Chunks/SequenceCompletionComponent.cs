using Ecs.Managers;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Chunks
{
    [Game]
    public class SequenceCompletionComponent : IComponent
    {
        public Uid Value;
    }
}