using Db.ChasePlayer;
using Db.Death;
using Db.Render.Glitch;
using Ecs.Action.Commands;
using Ecs.Utils;
using Game.Services.GameState;
using Game.Services.General;
using Game.Services.Render;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 945, nameof(EFeatures.Move))]
    public class ShowMonsterSystem : ForEachCommandUpdateSystem<ShowMonsterCommand>
    {
        private readonly IChasePlayerParameters _chasePlayerParameters;
        private readonly IDeadBlackoutParameters _deadBlackoutParameters;
        private readonly GameContext _game;
        private readonly IGameStateService _gameStateService;
        private readonly IGlitchParameters _glitchParameters;
        private readonly IGlitchService _glitchService;

        public ShowMonsterSystem(
            GameContext game,
            ICommandBuffer commandBuffer,
            IGlitchService glitchService,
            IGlitchParameters glitchParameters,
            IGameStateService gameStateService,
            IChasePlayerParameters chasePlayerParameters,
            IDeadBlackoutParameters deadBlackoutParameters
        ) : base(commandBuffer)
        {
            _game = game;
            _glitchService = glitchService;
            _glitchParameters = glitchParameters;
            _gameStateService = gameStateService;
            _chasePlayerParameters = chasePlayerParameters;
            _deadBlackoutParameters = deadBlackoutParameters;
        }

        protected override void Execute(ref ShowMonsterCommand command)
        {
            // Более показать не могу
        }

        private void PlayGlitch()
        {
            // Более показать не могу
        }
    }
}