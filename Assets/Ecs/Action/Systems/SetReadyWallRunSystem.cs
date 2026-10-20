using Db.Player;
using Ecs.Action.Commands;
using Ecs.Utils;
using Game.Services.Player;
using Game.Utils.Movement;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 945, nameof(EFeatures.Move))]
    public class SetReadyWallRunSystem : ForEachCommandUpdateSystem<SetReadyWallRunCommand>
    {
        private readonly GameContext _game;
        private readonly IPlayerMovementTypeService _playerMovementTypeService;
        private readonly IPlayerWallMotionParametersBase _playerWallMotionParameters;

        public SetReadyWallRunSystem(
            ICommandBuffer commandBuffer,
            GameContext game,
            IPlayerWallMotionParametersBase playerWallMotionParameters,
            IPlayerMovementTypeService playerMovementTypeService
        ) : base(commandBuffer)
        {
            _game = game;
            _playerWallMotionParameters = playerWallMotionParameters;
            _playerMovementTypeService = playerMovementTypeService;
        }

        protected override void Execute(ref SetReadyWallRunCommand command)
        {
            var player = _game.PlayerEntity;

            switch (command.IsReady)
            {
                case true when !player.HasWallRun:
                    var maxDuration = _playerWallMotionParameters.DurationPerUnit * command.Distance;
                    var wallRunParameters = new WallRunParameters(maxDuration);
                    player.ReplaceWallRun(wallRunParameters);
                    player.IsUseGravity = false;
                    _playerMovementTypeService.UpdateBodyType(player.Uid.Value);
                    break;

                case false when player.HasWallRun:
                    player.RemoveWallRun();
                    player.IsUseGravity = true;
                    _playerMovementTypeService.UpdateBodyType(player.Uid.Value);
                    break;
            }
        }
    }
}