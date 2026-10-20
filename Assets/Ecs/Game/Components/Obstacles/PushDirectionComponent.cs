using JCMG.EntitasRedux;
using UnityEngine;

namespace Ecs.Game.Components.Obstacles
{
    [Game]
    [Event(EventTarget.Self)]
    public class PushDirectionComponent : IComponent
    {
        public Vector3 Value;
    }
}