using Db.Camera;
using Game.Services.General;
using Game.Utils.Movement;

namespace Game.Services.Player.Impl
{
    public class PlayerTiltingService : IPlayerTiltingService
    {
        private readonly ICameraParameters _cameraParameters;
        private readonly IPlayerMovementTypeService _playerMovementTypeService;

        public PlayerTiltingService(
            ICameraParameters cameraParameters,
            IPlayerMovementTypeService playerMovementTypeService
        )
        {
            _cameraParameters = cameraParameters;
            _playerMovementTypeService = playerMovementTypeService;
        }

        public void SetActiveTiltingCamera(GameEntity player, float direction)
        {
            var tiltType = direction > 0 ? ETiltType.Right : ETiltType.Left;

            player.ReplaceTilting(tiltType);

            var duration = _cameraParameters.HorizontalTiltDuration;

            DelayedInvocationService.Invoke(
                () =>
                {
                    if (player.BodyMoveType.Value != EBodyMoveType.Run)
                        return;

                    CancelTilting(player);
                },
                duration
            );
        }

        public void CancelTilting(GameEntity player)
        {
            if (!player.HasTilting || player.Tilting.TiltType == ETiltType.None)
                return;

            player.ReplaceTilting(ETiltType.None);
            _playerMovementTypeService.UpdateBodyType(player.Uid.Value);
        }
    }
}