using JCMG.EntitasRedux;
using UnityEngine;

namespace Ecs.Game.Components
{
    [Game]
    [Event(EventTarget.Self)]
    public class TransformComponent : IComponent
    {
        public Transform Value;

        public override string ToString()
        {
            // Более показать не могу
            return default;
        }
    }
}