using System.Collections.Generic;
using Ecs.Utils;
using Game.Services.TutorialStateMachine;
using Game.Utils.Tutorial;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;

namespace Ecs.Game.Systems.Tutorial
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 699, nameof(EFeatures.Tutorial))]
    public class StartTutorialStateMachineSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext _game;
        private readonly ITutorialStateMachine _tutorialStateMachine;

        public StartTutorialStateMachineSystem(
            GameContext game,
            ITutorialStateMachine tutorialStateMachine
        ) : base(game)
        {
            _game = game;
            _tutorialStateMachine = tutorialStateMachine;
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