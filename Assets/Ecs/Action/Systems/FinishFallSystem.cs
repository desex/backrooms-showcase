using Db.SoundFxBase;
using Ecs.Action.Commands;
using Ecs.Utils;
using Game.Services.Player;
using Game.Services.Score;
using Game.Services.Sound;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 630, nameof(EFeatures.Move))]
    public class FinishFallSystem : ForEachCommandUpdateSystem<GroundedCommand>
    {
        private readonly GameContext _game;
        private readonly IPlayerMovementTypeService _playerMovementTypeService;
        private readonly IScoreService _scoreService;
        private readonly ISoundService _soundService;

        public FinishFallSystem(
            ICommandBuffer commandBuffer,
            GameContext game,
            IScoreService scoreService,
            ISoundService soundService,
            IPlayerMovementTypeService playerMovementTypeService
        ) : base(commandBuffer)
        {
            _game = game;
            _scoreService = scoreService;
            _soundService = soundService;
            _playerMovementTypeService = playerMovementTypeService;
        }

        protected override void Execute(ref GroundedCommand command)
        {
            if (_game.GameState.Value != EGameState.Game)
                return;

            var player = _game.PlayerEntity;
            player.ReplaceVerticalSpeed(0f);

            if (!player.IsReadyToFailFall)
                return;

            _scoreService.ResetComboAndMultiplier();
            _soundService.PlaySound(ESoundFxName.FallDown);
            _playerMovementTypeService.UpdateBodyType(player.Uid.Value);

            player.IsReadyToFailFall = false;
        }
    }
}