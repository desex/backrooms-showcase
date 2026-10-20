using JCMG.EntitasRedux;
using UnityEngine;

namespace Ecs.Game.Components.Chunks
{
    [Game]
    public class TargetRotationComponent : IComponent
    {
        public Quaternion Value;
    }
}