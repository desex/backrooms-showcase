using Ecs.Action.Commands.Movement;
using Ecs.Utils;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems.Movement
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 800, nameof(EFeatures.Move))]
    public class ChangeSpeedWithPlankObstacleSystem : ForEachCommandUpdateSystem<ChangeSpeedWithPlankObstacleCommand>
    {
        private readonly GameContext _game;

        public ChangeSpeedWithPlankObstacleSystem(ICommandBuffer commandBuffer, GameContext game) : base(commandBuffer)
        {
            _game = game;
        }

        protected override void Execute(ref ChangeSpeedWithPlankObstacleCommand command)
        {
            var character = _game.GetEntityWithUid(command.CharacterUid);

            if (command.IsEntering)
            {
                character.IsNeedSlowDown = true;
                return;
            }

            character.IsNeedSlowDown = false;
        }
    }
}