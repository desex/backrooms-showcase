using System.Collections.Generic;
using Ecs.Utils;
using Game.Services.Score;
using Game.Utils.Movement;
using Generated.Commands;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Game.Systems.Score
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 469, nameof(EFeatures.Score))]
    public class IncreaseScoreOnPlankRun : ReactiveSystem<GameEntity>
    {
        private readonly ICommandBuffer _commandBuffer;
        private readonly GameContext _game;
        private readonly IScoreService _scoreService;

        public IncreaseScoreOnPlankRun(
            GameContext game,
            IScoreService scoreService,
            ICommandBuffer commandBuffer
        ) : base(game)
        {
            _game = game;
            _scoreService = scoreService;
            _commandBuffer = commandBuffer;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            // Более показать не могу
            return default;
        }

        protected override bool Filter(GameEntity entity)
        {
            // Более показать не могу
            return default;
        }

        protected override void Execute(IEnumerable<GameEntity> entities)
        {
            // Более показать не могу
        }
    }
}