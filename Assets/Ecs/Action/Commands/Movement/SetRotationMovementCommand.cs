using Ecs.Commands.Generator;
using Ecs.Managers;
using UnityEngine;

namespace Ecs.Action.Commands.Movement
{
    [Command]
    public struct SetTargetRotationCommand
    {
        public Uid CharacterUid;
        public Quaternion Rotation;
    }
}