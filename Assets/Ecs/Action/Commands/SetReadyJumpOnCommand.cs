using Ecs.Commands.Generator;
using UnityEngine;

namespace Ecs.Action.Commands
{
    [Command]
    public struct SetReadyJumpOnCommand
    {
        public bool IsReady;
        public Vector3? JumpEndPoint;
        public Vector3? ObstacleForward;
    }
}