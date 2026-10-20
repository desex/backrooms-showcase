using JCMG.EntitasRedux;
using UnityEngine;

namespace Ecs.Game.Components.Movement
{
    [Game]
    public class ReadyToClimbComponent : IComponent
    {
        public Vector3 EndPoint;
    }
}