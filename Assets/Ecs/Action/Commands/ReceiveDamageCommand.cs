using Ecs.Commands.Generator;
using Ecs.Managers;
using UnityEngine;

namespace Ecs.Action.Commands
{
    [Command]
    public struct ReceiveDamageCommand
    {
        public Uid CharacterUid;
        public Vector3 HitDirection;
    }
}