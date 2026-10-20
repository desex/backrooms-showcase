using JCMG.EntitasRedux;
using UnityEngine;
using EventType = JCMG.EntitasRedux.EventType;

namespace Ecs.Game.Components
{
    [Game]
    [Event(EventTarget.Self, EventType.Added, 10)]
    public class LocalPositionComponent : IComponent
    {
        public Vector3 Value;
    }
}