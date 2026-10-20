#if !DISABLE_GHOSTS
using System.Collections.Generic;
using Db.Ghost;
using Db.Player;
using Ecs.Game.Extensions;
using Ecs.Utils;
using Game.Providers;
using Game.Services.Ghosts.Sound;
using Generated.Commands;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Game.Systems.Initialize
{
    [Install(ExecutionType.Game, ExecutionPriority.High, 30, nameof(EFeatures.Initialization))]
    public class GhostsInitializeSystem : IInitializeSystem
    {
        private readonly ICommandBuffer _commandBuffer;
        private readonly GameContext _game;

        public GhostsInitializeSystem(
            ICommandBuffer commandBuffer,
            GameContext game
        )
        {
            _commandBuffer = commandBuffer;
            _game = game;
        }

        public void Initialize()
        {
            if (_game.TutorialEntity.IsInTutorial)
                return;
            
            _commandBuffer.SpawnGhosts();
        }
    }
}
#endif