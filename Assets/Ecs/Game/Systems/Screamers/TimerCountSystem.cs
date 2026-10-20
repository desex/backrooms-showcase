using Db.Player;
using Db.Sound;
using Ecs.Core.Interfaces;
using Ecs.Utils;
using Ecs.Utils.Groups;
using Game.Services.Player;
using Game.Utils.Sound;
using Generated.Commands;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;
using UnityEngine;

namespace Ecs.Game.Systems.Screamers
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 1005, nameof(EFeatures.Common))]
    public class TimerCountSystem : APausableGameStateConditionUpdateSystem
    {
        private readonly IChaserSoundParameters _chaserSoundParameters;
        private readonly IGameGroupUtils _gameGroupUtils;
        private readonly ICommandBuffer _commandBuffer;
        private readonly ITimeProvider _timeProvider;
        private readonly GameContext _game;
        private readonly IPlayerSpeedService _playerSpeedService;
        private readonly IPlayerBasicMovementParametersBase _playerBasicMovementParametersBase;
        
        public TimerCountSystem(
            IChaserSoundParameters chaserSoundParameters,
            IGameGroupUtils gameGroupUtils,
            ICommandBuffer commandBuffer,
            ITimeProvider timeProvider,
            GameContext game,
            IPlayerSpeedService playerSpeedService,
            IPlayerBasicMovementParametersBase playerBasicMovementParametersBase
        ) : base(game)
        {
            _chaserSoundParameters = chaserSoundParameters;
            _gameGroupUtils = gameGroupUtils;
            _commandBuffer = commandBuffer;
            _timeProvider = timeProvider;
            _game = game;
            _playerSpeedService = playerSpeedService;
            _playerBasicMovementParametersBase = playerBasicMovementParametersBase;
        }

        protected override bool GameStateCondition(EGameState gameState) {
            // Более показать не могу
            return default;
        }

        protected override void UpdateSystem()
        {
            // Более показать не могу
        }

        private void UpdateScreamerTimer()
        {
            // Более показать не могу
        }

        private void UpdatePlayerInvulnerabilityTimer()
        {
            var player = _game.PlayerEntity;
            HandleInvulnerabilityTimer(player);

#if !DISABLE_GHOSTS
            using var _ = _gameGroupUtils.GetGhosts(out var ghosts);
            
            foreach (var ghost in ghosts)
                HandleInvulnerabilityTimer(ghost);
#endif
        }

        private void HandleInvulnerabilityTimer(GameEntity entity)
        {
            // Более показать не могу
        }

        private void UpdatePlayerDashTimer()
        {
            var player = _game.PlayerEntity;
            HandleDashTimer(player);

#if !DISABLE_GHOSTS
            using var _ = _gameGroupUtils.GetGhosts(out var ghosts);
            
            foreach (var ghost in ghosts) 
                HandleDashTimer(ghost);      
#endif
        }

        private void HandleDashTimer(GameEntity entity)
        {
            // Более показать не могу
        }

        private void ObstacleFeedbackAfter()
        {
            // Более показать не могу
        }

        private static void TurnTimerOff(ETimerType eTimerType, GameEntity timerEntity)
        {
            // Более показать не могу
        }

        private static void ChangeScreamerState(GameEntity timerEntity)
        {
            // Более показать не могу
        }

        private static void DisablePlayerInvulnerability(GameEntity timerEntity)
        {
            // Более показать не могу
        }
        
        private void UpdateRegenerationTimer()
        {
            // Более показать не могу
        }
        
        private void UpdateDamageVisualsTimer()
        {
            // Более показать не могу
        }

        private void UpdateReboundHorizontalSpeedTimer()
        {
            // Более показать не могу
        }

        private void UpdateAfterQteTimer()
        {
            // Более показать не могу
        }

        private void UpdateChaserRoarSoundTimer()
        {
            // Более показать не могу
        }
            
        private void UpdateQteLeftTime()
        {
            // Более показать не могу
        }
    }
}