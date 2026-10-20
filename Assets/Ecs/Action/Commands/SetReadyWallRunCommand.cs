using Ecs.Commands.Generator;
using Ecs.Managers;

namespace Ecs.Action.Commands
{
    [Command]
    public struct SetReadyWallRunCommand
    {
        public Uid CharacterUid;
        public bool IsReady;
        public float Distance;
    }
}