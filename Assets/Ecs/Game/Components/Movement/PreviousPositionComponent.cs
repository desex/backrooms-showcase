using JCMG.EntitasRedux;
using UnityEngine;

namespace Ecs.Game.Components.Movement
{
    [Game]
    public class PreviousPositionComponent : IComponent
    {
        public Vector3 Value;
    }
}