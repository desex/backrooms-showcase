using JCMG.EntitasRedux;
using UnityEngine;

namespace Ecs.Game.Components.Movement
{
    [Game]
    public class CollisionFlagsComponent : IComponent
    {
        public CollisionFlags Value;
    }
}