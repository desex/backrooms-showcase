using Db.Player;
using Db.SoundFxBase;
using Ecs.Utils;
using Game.Models.Camera;
using Game.Services.Player;
using Game.Services.Render;
using Game.Utils;
using Game.Utils.ColorsUtils;
using Game.Utils.Revive;
using Game.Utils.Sound;
using Generated.Commands;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems.Health
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 666, nameof(EFeatures.Common))]
    public class RevivePlayerSystem : AReviveSystem
    {
        private readonly IPlayerCameraHolder _cameraHolder;
        private readonly IGlitchService _glitchService;
        private readonly ICommandBuffer _commandBuffer;
        private readonly GameContext _game;
        private readonly IPlayerDeathParametersBase _playerDeathParametersBase;

        public RevivePlayerSystem(
            ICommandBuffer commandBuffer,
            GameContext game,
            IPlayerMovementTypeService movementTypeService,
            IPlayerCameraHolder cameraHolder,
            IGlitchService glitchService,
            IPlayerDeathParametersBase playerDeathParametersBase
        ) : base(commandBuffer, game, movementTypeService)
        {
            _cameraHolder = cameraHolder;
            _glitchService = glitchService;
            _commandBuffer = commandBuffer;
            _game = game;
            _playerDeathParametersBase = playerDeathParametersBase;
        }

        protected override bool IsNeedCharacter(GameEntity character)
        {
            return character.IsPlayer;
        }

        protected override void HandleRevive(GameEntity character)
        {
            character.IsCanSwipe = true;
            character.ReplaceScreamerPlayBackState(EPlayBackStatus.None, ESoundFxName.None);
            
            ResetQte(character);
            
            _cameraHolder.GetCamera().transform.SetParent(null);
            _cameraHolder.SwitchToGamePlayCamera();
            _glitchService.StopGlitchesImmediately();
            _commandBuffer.ResetSpeedShader();
            
            HideObstaclesNearRevivePoint(character);
            
            var isInTutorial = _game.TutorialEntity.IsInTutorial;

            if (isInTutorial)
                _commandBuffer.StartPlayerTutorialFailTeleport();
            else
                _commandBuffer.StartRevive();
        }

        private static void ResetQte(GameEntity player)
        {
            if (player.HasQteType)
                player.RemoveQteType();
            
            if (player.HasQteResult)
                player.RemoveQteResult();
        }

        private void HideObstaclesNearRevivePoint(GameEntity player)
        {
            if (!player.HasRevive)
            {
                DebugUtility.LogError($"In {nameof(RevivePlayerSystem)} player hasn't revive", UtilsColors.ErrorColor);
                return;
            }
            
            var reviveData = player.Revive.Value;
            
            var position = reviveData.ReviveType == EReviveType.Teleport
                ? reviveData.ReviveTransform.position
                : reviveData.MovePoints[^1].Point.position;
            
            var radius = _playerDeathParametersBase.HideObstaclesRadius;
            
            _commandBuffer.HideObstaclesInSphere(radius, position);
        }
    }
}