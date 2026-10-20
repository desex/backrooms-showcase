using Ecs.Action.Commands.Movement;
using Ecs.Utils;
using Game.Models.Camera;
using Game.Services.Camera;
using Game.Services.TimeScale;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;
using UnityEngine;

namespace Ecs.Action.Systems.Movement
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 260, nameof(EFeatures.Move))]
    public class CancelReviveCameraFocusSystem : ForEachCommandUpdateSystem<CancelReviveCameraFocusCommand>
    {
        private readonly ICameraService _cameraService;
        private readonly GameContext _gameContext;
        private readonly ISlowingDownReviveService _slowingDownReviveService;

        public CancelReviveCameraFocusSystem(
            ICommandBuffer commandBuffer,
            ICameraService cameraService,
            ISlowingDownReviveService slowingDownReviveService,
            GameContext gameContext
        ) : base(commandBuffer)
        {
            _cameraService = cameraService;
            _gameContext = gameContext;
            _slowingDownReviveService = slowingDownReviveService;
        }

        protected override void Execute(ref CancelReviveCameraFocusCommand command)
        {
            var player = _gameContext.PlayerEntity;

            player.IsInReviveZone = false;
            player.IsGravityDisabled = false;
            RenderSettings.fog = true;
            _slowingDownReviveService.CancelInterpolateTimeScale();
            _cameraService.SetActiveCamera(ECameraType.Run);
        }
    }
}