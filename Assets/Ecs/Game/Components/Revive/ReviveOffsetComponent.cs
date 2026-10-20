using JCMG.EntitasRedux;
using UnityEngine;

namespace Ecs.Game.Components.Revive
{
    [Game]
    public class ReviveOffsetComponent : IComponent
    {
        public Vector3 Value;
    }
}