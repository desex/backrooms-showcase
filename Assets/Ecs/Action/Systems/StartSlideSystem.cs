using Db.Player;
using Ecs.Action.Commands;
using Ecs.Utils;
using Ecs.Utils.Player;
using Game.Services.Player;
using Game.Utils.Movement;
using Game.Utils.MovementBlock;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 620, nameof(EFeatures.Move))]
    public class StartSlideSystem : ForEachCommandUpdateSystem<StartSlideCommand>
    {
        private readonly GameContext _game;
        private readonly IPlayerBasicMovementParametersBase _playerBasicMovementParametersBase;
        private readonly IPlayerMovementTypeService _playerMovementTypeService;
        private readonly IPlayerSlideAndJumpParameters _playerSlideParameters;
        private static IPlayerSizeService _playerSizeService;

        public StartSlideSystem(
            ICommandBuffer commandBuffer,
            GameContext game,
            IPlayerMovementTypeService playerMovementTypeService,
            IPlayerBasicMovementParametersBase playerBasicMovementParametersBase,
            IPlayerSlideAndJumpParameters playerSlideParameters,
            IPlayerSizeService playerSizeService
        ) : base(commandBuffer)
        {
            _game = game;
            _playerMovementTypeService = playerMovementTypeService;
            _playerBasicMovementParametersBase = playerBasicMovementParametersBase;
            _playerSlideParameters = playerSlideParameters;
            _playerSizeService = playerSizeService;
        }

        protected override void Execute(ref StartSlideCommand command)
        {
            var character = _game.GetEntityWithUid(command.CharacterUid);

            if (!CanUse(character))
                return;

            character.ReplaceSlideDuration(_playerSlideParameters.SlideTime);
            character.ReplaceSwipeDownCount(1);

            SetSlideType(character);

            _playerMovementTypeService.UpdateBodyType(character.Uid.Value);
        }

        private bool CanUse(GameEntity character)
        {
            var isSlideBlocked = character.IsPlayer
                                 && character.BlockedMovements.Value.IsMovementBlocked(EBlockedMovement.Slide);

            if (isSlideBlocked)
                return false;

            if (IsUpSurfaceMove(character))
                return false;

            if (IsGrounded(character))
                return false;
            
            if (character.HasJumping)
                return false;

            if (character.IsDash)
                return false;

            return true;
        }

        private bool IsUpSurfaceMove(GameEntity character)
        {
            if (character.HasReadyForControllableSlide)
                return false;

            if (character.HasSliding)
                return true;

            var minimumSlopeDifference = _playerBasicMovementParametersBase.MinimumSlopeDifference;
            var isUpSurfaceMove = character.CharacterController.Value.velocity.y > minimumSlopeDifference;

            return isUpSurfaceMove;
        }

        private static void SetSlideType(GameEntity character)
        {
            if (character.HasReadyForControllableSlide)
            {
                character.ReplaceSliding(ESlideType.Controllable);
                character.IsUseExtraGravity = true;
                _playerSizeService.SetCharacterControllerSize(EPlayerSizeType.ControllableSlide);
            }
            else
            {
                character.ReplaceSliding(ESlideType.Short);
                character.IsUseExtraGravity = false;
            }
        }

        private static bool IsGrounded(GameEntity character)
        {
            if (character.IsGrounded)
            {
                if (!character.IsReadyToFailFall)
                    return false;

                if (!character.HasReadyForControllableSlide)
                    return true;
            }
            else
            {
                if (!character.HasReadyForControllableSlide)
                    return true;
            }

            return false;
        }
    }
}