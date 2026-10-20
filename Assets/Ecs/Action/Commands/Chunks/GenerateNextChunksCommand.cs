using Ecs.Commands.Generator;
using Ecs.Managers;

namespace Ecs.Action.Commands.Chunks
{
    [Command]
    public struct GenerateNextChunksCommand
    {
        public Uid CurrentEnteredChunkId;
    }
}