using Db.Player;
using Db.SoundFxBase;
using Ecs.Core.Systems;
using Ecs.Utils;
using Ecs.Utils.Groups;
using Game.Services.Player;
using Game.Services.Sound;
using Game.Utils;
using Game.Utils.Chunks;
using Game.Utils.ColorsUtils;
using Game.Utils.Movement;
using Game.Utils.Overlaps;
using Generated.Commands;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Game.Systems.Movement
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 940, nameof(EFeatures.Move))]
    public class PlayerGroundedSystem : APausableCharacterConditionUpdateSystem
    {
        private readonly ICommandBuffer _commandBuffer;
        private readonly GameContext _game;
        private readonly IPlayerMovementTypeService _playerMovementTypeService;
        private readonly ISoundService _soundService;
        private readonly IOverlapService _overlapService;
        private readonly IPlayerBasicMovementParametersBase _playerBasicMovementParametersBase;

        public PlayerGroundedSystem(
            GameContext game,
            IGameGroupUtils gameGroupUtils,
            ICommandBuffer commandBuffer,
            IPlayerMovementTypeService playerMovementTypeService,
            ISoundService soundService,
            IOverlapService overlapService,
            IPlayerBasicMovementParametersBase playerBasicMovementParametersBase
        ) : base(game, gameGroupUtils)
        {
            _game = game;
            _commandBuffer = commandBuffer;
            _playerMovementTypeService = playerMovementTypeService;
            _soundService = soundService;
            _overlapService = overlapService;
            _playerBasicMovementParametersBase = playerBasicMovementParametersBase;
        }
        
        protected override bool EntityFilter(GameEntity entity)
        {
            return entity.IsPlayer;
        }
        
        protected override void HandleUpdate(GameEntity entity)
        {
            var playerEntity = _game.PlayerEntity;
            
            var position = playerEntity.Position.Value + _playerBasicMovementParametersBase.GroundedSphereOffset;
            
            var isGrounded = _overlapService.CheckOverlapSphere(position,
                                                                _playerBasicMovementParametersBase.GroundedSphereRadius,
                                                                LayerMasks.GroundedMask
            );
            
            var wasPlayerGrounded = entity.IsGrounded;
            entity.IsGrounded = isGrounded;

            if (!playerEntity.HasCurrentChunkId)
                return;

            var chunkUid = _game.PlayerEntity.CurrentChunkId.Value;
            var chunkEntity = _game.GetEntityWithUid(chunkUid);
            var chunkNode = chunkEntity.ChunkNode.Value;
            var inDownChunk = (chunkNode.ChunkType & EChunkType.Down) != 0;

            CheckEntityValid(entity, chunkEntity, chunkNode);

            if (inDownChunk)
            {
                TryUpdateBodyMoveType();
                entity.IsFallInterrupted = false;
                TrySlidePlayer();
                TryPlayGroundedSound();
                return;
            }

            TryLand();
            TrySlidePlayer();
            TryPlayGroundedSound();

            return;

            void TryLand()
            {
                if (!isGrounded || wasPlayerGrounded || !entity.IsFallInterrupted)
                    return;
                
                _playerMovementTypeService.UpdateBodyType(entity.Uid.Value);
                entity.IsFallInterrupted = false;
                _commandBuffer.Landing();
            }

            void TryPlayGroundedSound()
            {
                if (isGrounded
                    && !wasPlayerGrounded
                    && !entity.IsFallInterrupted
                    && !entity.IsDead
                    && entity.BodyMoveType.Value != EBodyMoveType.Roll)
                    _soundService.PlaySound(ESoundFxName.Landing);
            }

            void TrySlidePlayer()
            {
                if (!isGrounded || wasPlayerGrounded || !entity.HasReadyForControllableSlide)
                    return;

                _commandBuffer.StartSlide(entity.Uid.Value);
            }

            void TryUpdateBodyMoveType()
            {
                if (isGrounded && wasPlayerGrounded)
                    return;
                
                _playerMovementTypeService.UpdateBodyType(entity.Uid.Value);
            }
        }
        

        private static void CheckEntityValid(GameEntity entity, GameEntity chunkEntity, ChunkNode chunkNode)
        {
            if (entity == null)
                DebugUtility.LogError($"In {nameof(PlayerGroundedSystem)} entity is NUll", UtilsColors.ErrorColor);

            if (chunkEntity == null)
                DebugUtility.LogError($"In {nameof(PlayerGroundedSystem)} chunkEntity is NUll", UtilsColors.ErrorColor);

            if (chunkNode == null)
                DebugUtility.LogError($"In {nameof(PlayerGroundedSystem)} chunkNode is NUll", UtilsColors.ErrorColor);
        }
    }
}