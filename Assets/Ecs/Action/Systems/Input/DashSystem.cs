using Db.Player;
using Db.SoundFxBase;
using Ecs.Action.Commands.Input;
using Ecs.Utils;
using Ecs.Utils.ItemCollectables;
using Game.Services.CollectablesItem;
using Game.Services.Render;
using Game.Services.Sound;
using Game.Utils.Chunks;
using Game.Utils.Collisions;
using Game.Utils.Movement;
using Game.Utils.MovementBlock;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems.Input
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 500, nameof(EFeatures.Input))]
    public class DashSystem : ForEachCommandUpdateSystem<DashCommand>
    {
        private readonly ICollectiblesStorage _collectibles;
        private readonly ICollisionService _collisionService;
        private readonly IDashParameters _dashParameters;
        private readonly GameContext _game;
        private readonly ISoundService _soundService;
        private readonly ISpeedEffectService _speedEffectService;

        public DashSystem(
            ISpeedEffectService speedEffectService,
            ICommandBuffer commandBuffer,
            ISoundService soundService,
            GameContext game,
            IDashParameters dashParameters,
            ICollectiblesStorage collectibles,
            ICollisionService collisionService
        ) : base(commandBuffer)
        {
            _speedEffectService = speedEffectService;
            _soundService = soundService;
            _game = game;
            _dashParameters = dashParameters;
            _collectibles = collectibles;
            _collisionService = collisionService;
        }

        protected override void Execute(ref DashCommand command)
        {
            var player = _game.PlayerEntity;

            if (!CanUse(player, command))
                return;

            if (!command.Value)
            {
                EndDash();
                return;
            }

            if (!TryStartDash(player))
                return;

            StartDash(player);
        }

        private bool TryStartDash(GameEntity player)
        {
            if (!CanDashInProperChunkType(player))
                return false;

            if (player.BlockedMovements.Value.IsMovementBlocked(EBlockedMovement.Dash))
                return false;
            
            if (player.IsInterruptPlayerMovement)
                return false;

            if (player.IsGrounded)
                return true;

            if (player.HasJumping)
                if (player.Jumping.Value == EJumpType.DoubleJump)
                    return false;

            if (!IsJumping(player))
                return false;

            return true;
        }

        private bool IsJumping(GameEntity player)
        {
            if (player.HasWallRun)
                return false;

            if (player.HasWallType)
                return false;

            if (!player.HasJumping)
                return false;

            if (player.Jumping.Value is not (EJumpType.Jump or EJumpType.DoubleJump))
                return false;

            return true;
        }

        private bool CanUse(GameEntity player, DashCommand command)
        {
            if (player.HasSlideChunk)
                return false;
            
            if (!CanDashInBodyMoveType(player))
                return false;

            if (player.IsBerserkMode)
                return false;

            var almondWaterPrice = _dashParameters.DashVo.AlmondWaterPrice;

            if (command.Value && !_collectibles.HasCollectibleItem(ECollectableType.AlmondWater, almondWaterPrice))
                return false;

            return true;
        }

        private float GetSpeed(GameEntity player)
        {
            var dashVo = _dashParameters.DashVo;

            if (player.IsGrounded)
                return dashVo.Speed;

            return player.Jumping.Value switch
            {
                EJumpType.Jump => dashVo.JumpSpeed,
                EJumpType.DoubleJump => dashVo.SpeedOnDoubleJump,
                _ => dashVo.Speed,
            };
        }

        private void StartDash(GameEntity player)
        {
            var dashPrice = _dashParameters.DashVo.AlmondWaterPrice;
            var duration = _dashParameters.DashVo.Duration;
            var speed = GetSpeed(player);

            player.IsUseGravity = false;
            player.IsDash = true;
            _soundService.PlaySound(ESoundFxName.Dash);

            //TODO: additional I think unnecessary check but it is =)
            if (!player.HasPlayerDashTimer)
                player.AddPlayerDashTimer(duration);
            else
                player.ReplacePlayerDashTimer(duration);

            player.ReplaceBerserkSpeedMultiplier(speed);
            _collectibles.RemoveCollectibleItem(ECollectableType.AlmondWater, dashPrice);

            if (!player.HasQteType)
                _speedEffectService.EnableSpeedEffect();
            
            _collisionService.DisablePlayerCollisions();
        }

        private void EndDash()
        {
            var player = _game.PlayerEntity;
            var dashVo = _dashParameters.DashVo;

            player.IsUseGravity = true;
            player.IsDash = false;

            player.ReplaceBerserkSpeedMultiplier(dashVo.DefaultSpeed);
            player.ReplaceVerticalSpeed(dashVo.VerticalSpeed);

            _speedEffectService.DisableSpeedEffect();
            _collisionService.EnablePlayerCollisions();
        }

        private static bool CanDashInBodyMoveType(GameEntity player)
        {
            var type = player.BodyMoveType.Value;

            if (type == EBodyMoveType.Run)
                return true;

            if (type == EBodyMoveType.Jump)
                return true;

            if (type == EBodyMoveType.InterruptFall)
                return true;

            if (type == EBodyMoveType.HitOnObstacle)
                return true;

            return false;
        }

        private bool CanDashInProperChunkType(GameEntity player)
        {
            if (!player.HasCurrentChunkId)
                return false;

            var currentChunk = player.CurrentChunkId.Value;

            var chunkType = _game.GetEntityWithUid(currentChunk).ChunkNode.Value.ChunkType;

            if (chunkType == EChunkType.Transition)
                return false;

            return true;
        }
    }
}