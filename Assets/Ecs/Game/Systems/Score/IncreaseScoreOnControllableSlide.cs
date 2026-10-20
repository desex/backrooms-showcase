using System;
using Ecs.Game.Components.Movement;
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
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 691, nameof(EFeatures.Score))]
    public class IncreaseScoreOnControllableSlide
        : ComponentReplacedReactiveSystem<GameEntity, SlidingComponent, ESlideType>
    {
        private readonly ICommandBuffer _commandBuffer;
        private readonly GameContext _game;
        private readonly IScoreService _scoreService;

        public IncreaseScoreOnControllableSlide(
            GameContext game,
            IScoreService scoreService,
            ICommandBuffer commandBuffer
        ) : base(game)
        {
            _game = game;
            _scoreService = scoreService;
            _commandBuffer = commandBuffer;
        }

        protected override Type[] GetComponentTypes()
        {
            // Более показать не могу
            return global::System.Array.Empty<Type>();
        }

        protected override ESlideType GetValue(SlidingComponent component)
        {
            // Более показать не могу
            return default;
        }

        protected override void OnReplaced(GameEntity entity, ESlideType last, ESlideType next)
        {
            // Более показать не могу
        }
    }
}