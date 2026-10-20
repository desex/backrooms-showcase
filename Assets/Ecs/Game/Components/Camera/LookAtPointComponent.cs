using JCMG.EntitasRedux;
using UnityEngine;

namespace Ecs.Game.Components.Camera
{
    [Game]
    public class LookAtPointComponent : IComponent
    {
        public Transform Value;
    }
}