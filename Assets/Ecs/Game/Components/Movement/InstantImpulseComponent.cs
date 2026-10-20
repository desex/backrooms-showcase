using JCMG.EntitasRedux;
using UnityEngine;

namespace Ecs.Game.Components.Movement
{
    [Game]
    public class InstantImpulseComponent : IComponent
    {
        public Vector3 Value;
    }
}