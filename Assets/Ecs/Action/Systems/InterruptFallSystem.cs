using Db.Player;
using Ecs.Action.Commands;
using Ecs.Utils;
using Game.Services.Player;
using Game.Services.Score;
using Game.Utils.Movement;
using Generated.Commands;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 610, nameof(EFeatures.Move))]
    public class InterruptFallSystem : ForEachCommandUpdateSystem<InterruptFallCommand>
    {
        private readonly ICommandBuffer _commandBuffer;
        private readonly GameContext _game;
        private readonly IPlayerBasicMovementParametersBase _playerBasicMovementParametersBase;
        private readonly IPlayerMovementTypeService _playerMovementTypeService;
        private readonly IScoreService _scoreService;

        public InterruptFallSystem(
            IPlayerBasicMovementParametersBase playerBasicMovementParametersBase,
            IPlayerMovementTypeService playerMovementTypeService,
            ICommandBuffer commandBuffer,
            IScoreService scoreService,
            GameContext game
        ) : base(commandBuffer)
        {
            _commandBuffer = commandBuffer;
            _scoreService = scoreService;
            _game = game;
            _playerMovementTypeService = playerMovementTypeService;
            _playerBasicMovementParametersBase = playerBasicMovementParametersBase;
        }

        protected override void Execute(ref InterruptFallCommand command)
        {
            var player = _game.PlayerEntity;
            var isFallInterrupted = player.IsFallInterrupted;
            var isInTutorial = _game.TutorialEntity.IsInTutorial;
            var isQte = player.HasQteType;
            
            if (isFallInterrupted || isInTutorial || isQte)
                return;

            player.IsReadyToFailFall = false;
            player.IsFallInterrupted = true;

            var canInterruptJump = player.HasJumping && player.Jumping.Value is EJumpType.Jump or EJumpType.DoubleJump;

            if (canInterruptJump || player.IsEnteredTransitionHole)
            {
                var verticalSpeed = _playerBasicMovementParametersBase.GravityForce;
                player.ReplaceVerticalSpeed(verticalSpeed);
            }

            if (canInterruptJump)
                player.RemoveJumping();

            if (player.IsDash)
                _commandBuffer.Dash(false);

            if (player.IsEnteredTransitionHole)
                _scoreService.HandleSuccessAction(EScoreActionType.ActionWithoutZones, command.CharacterUid);

            _playerMovementTypeService.UpdateBodyType(player.Uid.Value);
        }
    }
}