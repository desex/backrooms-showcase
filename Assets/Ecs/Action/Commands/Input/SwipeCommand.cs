using Ecs.Commands.Generator;
using UnityEngine;

namespace Ecs.Action.Commands.Input
{
    [Command]
    public struct SwipeCommand
    {
        public Vector2 Direction;
    }
}