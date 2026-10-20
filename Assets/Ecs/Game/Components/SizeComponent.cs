using JCMG.EntitasRedux;
using UnityEngine;

namespace Ecs.Game.Components
{
    [Game]
    public class SizeComponent : IComponent
    {
        public Vector3 Value;
    }
}