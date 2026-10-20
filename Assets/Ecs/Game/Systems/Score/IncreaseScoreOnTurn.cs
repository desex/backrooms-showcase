using System.Collections.Generic;
using Ecs.Utils;
using Game.Services.Score;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;

namespace Ecs.Game.Systems.Score
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 769, nameof(EFeatures.Score))]
    public class IncreaseScoreOnTurn : ReactiveSystem<GameEntity>
    {
        private readonly GameContext _game;
        private readonly IScoreService _scoreService;

        public IncreaseScoreOnTurn(
            GameContext game,
            IScoreService scoreService
        ) : base(game)
        {
            _game = game;
            _scoreService = scoreService;
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