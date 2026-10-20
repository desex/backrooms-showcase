using Ecs.Commands.Generator;
using UnityEngine;

namespace Ecs.Action.Commands
{
    [Command]
    public struct StartClimbInHoleCommand
    {
        public Vector3 ClimbEndPoint;
    }
}