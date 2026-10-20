using Ecs.Commands.Generator;
using UnityEngine;

namespace Ecs.Action.Commands.Obstacles
{
    [Command]
    public struct HideObstaclesInSphereCommand
    {
        public float Radius;
        public Vector3 Position;
    }
}