using Ecs.Action.Commands;
using Ecs.Utils;
using Game.Services.Camera;
using Game.Services.Player;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 614, nameof(EFeatures.Move))]
    public class SetMovingUpSurfaceSystem : ForEachCommandUpdateSystem<SetMovingUpSurfaceCommand>
    {
        private readonly ICameraService _cameraService;
        private readonly GameContext _game;
        private readonly IPlayerMovementTypeService _playerMovementTypeService;

        public SetMovingUpSurfaceSystem(
            ICommandBuffer commandBuffer,
            GameContext game,
            IPlayerMovementTypeService playerMovementTypeService,
            ICameraService cameraService
        ) : base(commandBuffer)
        {
            _game = game;
            _playerMovementTypeService = playerMovementTypeService;
            _cameraService = cameraService;
        }

        protected override void Execute(ref SetMovingUpSurfaceCommand command)
        {
            var player = _game.PlayerEntity;
            player.IsMovingUpSurface = command.IsMovingUpSurface;

            if (command.IsMovingUpSurface)
                _cameraService.RotateCameraUp();
            else
                _cameraService.RotateCameraToDefault();

            _playerMovementTypeService.UpdateBodyType(player.Uid.Value);
        }
    }
}