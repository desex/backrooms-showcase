using Ecs.Commands.Generator;
using Ecs.Managers;

namespace Ecs.Action.Commands.Chunks
{
    [Command]
    public struct DeletePrePreviousChunkCommand
    {
        public Uid CurrentEnteredChunkId;
    }
}