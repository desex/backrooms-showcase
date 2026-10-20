using Ecs.Commands.Generator;
using UnityEngine;

namespace Ecs.Action.Commands.Obstacles
{
    [Command]
    public struct HandleWaterEnterCommand
    {
        public Vector3 WaterStartPosition;
    }
}