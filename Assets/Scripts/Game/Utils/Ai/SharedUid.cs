using BehaviorDesigner.Runtime;
using Ecs.Managers;

namespace Game.Utils.Ai
{
    [System.Serializable]
    public class SharedUid : SharedVariable<Uid>
    {
        public static implicit operator SharedUid(Uid value) { return new() { Value = value }; }
    }
} 