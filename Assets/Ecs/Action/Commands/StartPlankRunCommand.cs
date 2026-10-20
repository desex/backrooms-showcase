using Ecs.Commands.Generator;
using Ecs.Managers;
using UnityEngine;

namespace Ecs.Action.Commands
{
    [Command]
    public struct StartPlankRunCommand
    {
        public Uid CharacterUid;
        public Vector3 StartPoint;
    }
}