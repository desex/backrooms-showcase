using Ecs.Action.Commands;
using Ecs.Utils;
using Game.Utils.Movement;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 96, nameof(EFeatures.Move))]
    public class SetReadyJumpOnSystem : ForEachCommandUpdateSystem<SetReadyJumpOnCommand>
    {
        private readonly GameContext _game;

        public SetReadyJumpOnSystem(ICommandBuffer commandBuffer, GameContext game) : base(commandBuffer)
        {
            _game = game;
        }

        protected override void Execute(ref SetReadyJumpOnCommand command)
        {
            var player = _game.PlayerEntity;

            if (!player.IsGrounded
                || !command.IsReady
                || command.JumpEndPoint is null
                || command.ObstacleForward is null)
                return;

            var jumpEndPoint = command.JumpEndPoint.Value;
            var obstacleForward = command.ObstacleForward.Value;
            var jumpOnParameters = new JumpOnParameters(jumpEndPoint, obstacleForward);
            player.ReplaceReadyToJumpOn(jumpOnParameters);
        }
    }
}