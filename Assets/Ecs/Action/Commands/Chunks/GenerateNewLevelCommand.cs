using Ecs.Commands.Generator;
using Game.Utils;

namespace Ecs.Action.Commands.Chunks
{
    [Command]
    public struct GenerateNewLevelCommand
    {
        public EGameLevelType LevelType;
    }
}