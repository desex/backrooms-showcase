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
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 621, nameof(EFeatures.Move))]
    public class ContinueSlideSystem : ForEachCommandUpdateSystem<ContinueSlideCommand>
    {
        private readonly GameContext _game;
        private readonly IPlayerBasicMovementParametersBase _playerBasicMovementParametersBase;
        private readonly IPlayerMovementTypeService _playerMovementTypeService;
        private readonly IPlayerSlideService _playerSlideService;

        public ContinueSlideSystem(
            ICommandBuffer commandBuffer,
            GameContext game,
            IPlayerBasicMovementParametersBase playerBasicMovementParametersBase,
            IPlayerSlideService playerSlideService,
            IPlayerMovementTypeService playerMovementTypeService
        ) : base(commandBuffer)
        {
            _game = game;
            _playerBasicMovementParametersBase = playerBasicMovementParametersBase;
            _playerSlideService = playerSlideService;
            _playerMovementTypeService = playerMovementTypeService;
        }

        protected override void Execute(ref ContinueSlideCommand command)
        {
            var player = _game.PlayerEntity;

            if (!IsReadyForContinueSlide(player))
                return;

            var swipeCount = _playerSlideService.SwipeDownCount();

            player.ReplaceSwipeDownCount(swipeCount + 1);

            if (!_playerSlideService.IsSlideAvailable(player.Uid.Value))
                return;

            if (!player.HasReadyForControllableSlide)
                return;

            player.ReplaceSliding(ESlideType.Controllable);
            player.IsUseExtraGravity = true;
            _playerMovementTypeService.UpdateBodyType(player.Uid.Value);
        }

        private bool IsReadyForContinueSlide(GameEntity player)
        {
            var isSliding = player.HasSliding;
            var hasSlideDuration = player.HasSlideDuration;

            if (!isSliding || !hasSlideDuration)
                return false;

            var slideTime = player.SlideDuration.Value;
            var isGrounded = player.IsGrounded;
            var isReadyForControllableSlide = player.HasReadyForControllableSlide;
            var isReadyToFailFall = player.IsReadyToFailFall;

            var minimumSlopeDifference = _playerBasicMovementParametersBase.MinimumSlopeDifference;
            var isUpSurfaceMove = player.CharacterController.Value.velocity.y > minimumSlopeDifference;

            if (slideTime <= 0)
                return false;

            if (!isReadyForControllableSlide && isUpSurfaceMove)
                return false;

            if ((!isGrounded || isReadyToFailFall) && !isReadyForControllableSlide)
                return false;

            return true;
        }
    }
}