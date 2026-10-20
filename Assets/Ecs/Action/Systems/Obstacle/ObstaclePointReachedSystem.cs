using Ecs.Action.Commands.Obstacles.Destructibles;
using Ecs.Utils;
using Game.Utils.Obstacles;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems.Obstacle
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 590, nameof(EFeatures.Move))]
    public class ObstaclePointReachedSystem : ForEachCommandUpdateSystem<ObstaclePointReachedCommand>
    {
        private readonly GameContext _game;

        public ObstaclePointReachedSystem(ICommandBuffer commandBuffer, GameContext game) : base(commandBuffer)
        {
            _game = game;
        }

        protected override void Execute(ref ObstaclePointReachedCommand command)
        {
            var obstacle = _game.GetEntityWithUid(command.ObstacleUid);

            if (obstacle == null || obstacle.IsDestroyed)
                return;

            var data = new MovePointReachedData(command.PointIndex, command.CharacterUid);
            obstacle.ReplaceObstacleMovePointReached(data);
        }
    }
}