using BehaviorDesigner.Runtime;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Ai
{
    [Game]
    public class BTreeComponent : IComponent
    {
        public BehaviorTree Value;
    }
}