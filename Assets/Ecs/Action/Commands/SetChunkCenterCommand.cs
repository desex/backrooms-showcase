using Ecs.Commands.Generator;
using Ecs.Managers;
using UnityEngine;

namespace Ecs.Action.Commands
{
    [Command]
    public struct SetChunkCenterCommand
    {
        public Uid CharacterUid;
        public Vector3 Center;
    }
}