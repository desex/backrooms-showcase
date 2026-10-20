using JCMG.EntitasRedux;
using UnityEngine;

namespace Ecs.Game.Components.Player
{
    [Game]
    [Event(EventTarget.Self)]
    public class DamageReboundDirectionComponent : IComponent
    {
        public Vector3 Value;
    }
}