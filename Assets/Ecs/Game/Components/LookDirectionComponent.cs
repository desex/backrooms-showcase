using JCMG.EntitasRedux;
using UnityEngine;

namespace Ecs.Game.Components
{
    [Game]
    public class LookDirectionComponent : IComponent
    {
        public Vector3 Value;
    }
}