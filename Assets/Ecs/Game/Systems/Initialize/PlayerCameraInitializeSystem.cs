using System.Collections.Generic;
using Ecs.Utils;
using Ecs.Views.Linkable.Impl.Player;
using Game.Models.Camera;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;

namespace Ecs.Game.Systems.Initialize
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 750, nameof(EFeatures.Initialization))]
    public class PlayerCameraInitializeSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext _game;
        private readonly IPlayerCameraHolder _playerCameraHolder;

        public PlayerCameraInitializeSystem(
            GameContext game,
            IPlayerCameraHolder playerCameraHolder
        ) : base(game)
        {
            _game = game;
            _playerCameraHolder = playerCameraHolder;
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