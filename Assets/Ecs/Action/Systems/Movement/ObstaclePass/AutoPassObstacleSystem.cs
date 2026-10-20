using Ecs.Action.Commands.Movement.ObstaclePass;
using Ecs.Utils;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems.Movement.ObstaclePass
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 970, nameof(EFeatures.Move))]
    public class AutoPassObstacleSystem : ForEachCommandUpdateSystem<AutoPassObstacleCommand>
    {
        private readonly GameContext _game;

        public AutoPassObstacleSystem(ICommandBuffer commandBuffer, GameContext game) : base(commandBuffer)
        {
            _game = game;
        }

        protected override void Execute(ref AutoPassObstacleCommand command)
        {
            var character = _game.GetEntityWithUid(command.CharacterUid);

            character.ReplaceObstaclePassReact(true);
            character.IsCanSwipe = false;
            character.IsCanMoveHorizontally = false;
        }
    }
}