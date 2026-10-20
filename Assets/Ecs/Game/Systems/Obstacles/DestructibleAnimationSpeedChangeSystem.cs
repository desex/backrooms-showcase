using Db.Obstacles.DestructibleObstacles;
using DG.Tweening;
using Ecs.Action.Commands.Obstacles.Destructibles;
using Ecs.Utils;
using Game.Services.Player;
using Game.Utils.Movement;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Game.Systems.Obstacles
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 920, nameof(EFeatures.Move))]
    public class DestructibleAnimationSpeedChangeSystem : ForEachCommandUpdateSystem<DestructibleAccelerationCommand>
    {
        private readonly IDestructibleObstaclesParameters _destructibleObstaclesParameters;
        private readonly GameContext _game;
        private readonly IPlayerSpeedService _playerSpeedService;

        public DestructibleAnimationSpeedChangeSystem(
            IDestructibleObstaclesParameters destructibleObstaclesParameters,
            ICommandBuffer commandBuffer,
            IPlayerSpeedService playerSpeedService,
            GameContext game
        ) : base(commandBuffer)
        {
            _destructibleObstaclesParameters = destructibleObstaclesParameters;
            _playerSpeedService = playerSpeedService;
            _game = game;
        }

        protected override void Execute(ref DestructibleAccelerationCommand command)
        {
            // Более показать не могу
        }

        private void KillTweenIfDashStart(Tween tween, GameEntity character, float cachedCurrentForwardSpeed)
        {
            // Более показать не могу
        }
    }
}