using JCMG.EntitasRedux;
using UnityEngine;

namespace Ecs.Game.Components
{
    [Game]
    public class RigidbodyComponent : IComponent
    {
        public Rigidbody Value;
    }
}