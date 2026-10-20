using Ecs.Action.Commands.Movement.ObstaclePass;
using Ecs.Utils;
using Game.Services.Effects;
using Game.Services.Player;
using Game.Services.Score;
using Game.Utils.Movement;
using Generated.Commands;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;
using UnityEngine;

namespace Ecs.Action.Systems.Movement.ObstaclePass
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 271, nameof(EFeatures.Move))]
    public class FinishPassObstacleSystem : ForEachCommandUpdateSystem<FinishPassObstacleCommand>
    {
        private readonly IAccelerationFactorService _accelerationFactorService;
        private readonly ICommandBuffer _commandBuffer;
        private readonly GameContext _game;
        private readonly IPlayerMovementTypeService _playerMovementTypeService;
        private readonly IScoreService _scoreService;
        private readonly IUnderWaterRendererService _underWaterRendererService;

        public FinishPassObstacleSystem(
            ICommandBuffer commandBuffer,
            GameContext game,
            IPlayerMovementTypeService playerMovementTypeService,
            IAccelerationFactorService accelerationFactorService,
            IScoreService scoreService,
            IUnderWaterRendererService underWaterRendererService
        ) : base(commandBuffer)
        {
            _commandBuffer = commandBuffer;
            _game = game;
            _playerMovementTypeService = playerMovementTypeService;
            _accelerationFactorService = accelerationFactorService;
            _scoreService = scoreService;
            _underWaterRendererService = underWaterRendererService;
        }

        protected override void Execute(ref FinishPassObstacleCommand command)
        {
            var character = _game.GetEntityWithUid(command.CharacterUid);
            var playerTransform = character.Transform.Value;

            if (character.HasPassObstacleMovePoints)
                character.RemovePassObstacleMovePoints();

            if (character.HasVelocity)
                character.Velocity.Value = Vector3.zero;

            var isFinishingDestructible = false;

            if (character.HasReadyToObstaclePass)
            {
                var readyToObstaclePassData = character.ReadyToObstaclePass.Value;
                isFinishingDestructible = readyToObstaclePassData.PassType == EObstaclePassType.Destructible;
                character.RemoveReadyToObstaclePass();
            }

            if (character.HasObstaclePassReact)
                character.RemoveObstaclePassReact();

            if (character.HasDashWhenInObstacle)
                character.RemoveDashWhenInObstacle();

            if (character.HasJumping && command.WasCompleted)
                character.RemoveJumping();

            if (character.HasSliding && !character.HasSlideDuration)
                character.RemoveSliding();

            if (character.IsReadyForFastActionMove && !character.IsDamaged && _scoreService.HasCombo.CurrentValue)
            {
                character.IsSpeedBoost = true;
                _accelerationFactorService.InterpolateAccelerationFactor();
            }

            character.IsHitOnObstacle = false;
            character.IsReadyForFastActionMove = false;
            character.Position.Value = playerTransform.position;

            character.CharacterController.Value.enabled = true;
            character.IsEnableCharacterTrigger = true;

            character.IsUseGravity = true;
            character.IsCanSwipe = true;
            character.IsCanBeDamaged = true;
            character.IsCanMoveHorizontally = true;
            character.IsCanDash = false;
            character.IsComebackInHole = false;
            character.IsComeback = false;
            character.IsClimbing = false;
            character.IsDashInHole = false;

            // fix not removing jump boost after disabling character controller and comeback
            character.IsInWideHole = false;

            if (character.IsDead)
                return;

            if (command.WasCompleted && isFinishingDestructible)
                _commandBuffer.DestructibleAcceleration(command.CharacterUid);

            if (character.IsPlayer && !character.IsNeedSlowDown && !character.IsComebackInHole)
                _commandBuffer.ActivationExplosionEffect(true);

            if ((character.IsFallInterrupted || character.BodyMoveType.Value == EBodyMoveType.Roll)
                && !character.IsInWater)
                return;

            if (character.IsInWater && command.WasCompleted)
            {
                character.IsInWater = false;
                character.IsFallInterrupted = false;
                _underWaterRendererService.ResetUnderWaterLevel();
            }

            _playerMovementTypeService.UpdateBodyType(character.Uid.Value);
        }
    }
}