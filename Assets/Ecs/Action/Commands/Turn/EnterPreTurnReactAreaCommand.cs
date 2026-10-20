using Ecs.Commands.Generator;
using Ecs.Managers;
using Game.Utils.Movement;

namespace Ecs.Action.Commands.Turn
{
    [Command]
    public struct EnterPreTurnReactAreaCommand
    {
        public Uid ChunkUid;
        public ETurnDirection TurnDirection;
    }
}