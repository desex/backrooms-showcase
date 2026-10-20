using System;
using Db.SoundFxBase;
using Ecs.Action.Commands;
using Ecs.Utils;
using Game.Services.Analytics;
using Game.Services.GameState;
using Game.Services.General;
using Game.Services.Ghosts.Sound;
using Game.Services.Obstacles;
using Game.Services.Sound;
using Game.Utils.Sound;
using Generated.Commands;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 941, nameof(EFeatures.Move))]
    public class KillPlayerSystem : ForEachCommandUpdateSystem<KillPlayerCommand>
    {
        private readonly IAnalyticsService _analyticsService;
        private readonly ICommandBuffer _commandBuffer;
        private readonly GameContext _game;
        private readonly IGameStateService _gameStateService;
        private readonly IGhostSoundDispatcher _ghostSoundDispatcher;
        private readonly IObstacleGenerationService _obstacleGenerationService;
        private readonly ISoundService _soundService;
        private readonly ISoundSettingsBase _soundSettingsBase;

        public KillPlayerSystem(
            ICommandBuffer commandBuffer,
            IGameStateService gameStateService,
            ISoundService soundService,
            ISoundSettingsBase soundSettingsBase,
            IObstacleGenerationService obstacleGenerationService,
            IGhostSoundDispatcher ghostSoundDispatcher,
            IAnalyticsService analyticsService,
            GameContext game
        ) : base(commandBuffer)
        {
            _commandBuffer = commandBuffer;
            _game = game;
            _gameStateService = gameStateService;
            _soundService = soundService;
            _soundSettingsBase = soundSettingsBase;
            _obstacleGenerationService = obstacleGenerationService;
            _ghostSoundDispatcher = ghostSoundDispatcher;
            _analyticsService = analyticsService;
        }

        protected override void Execute(ref KillPlayerCommand command)
        {
            var entityUid = command.CharacterUid;
            var entity = _game.GetEntityWithUid(entityUid);

            if (entity.IsDead)
                return;

            var isInTutorial = _game.TutorialEntity.IsInTutorial;

            if (isInTutorial && entity.IsTutorialFailTeleporting)
                return;

            entity.IsDead = true;
            entity.IsObstaclePassCondition = false;
            entity.IsCaughtByHand = false;

            if (entity.IsPlayer)
            {
                _obstacleGenerationService.ResetObstaclesProgression();

                _soundService.StopSound(ESoundFxName.Run);

                if (entity.IsBerserkMode)
                    _commandBuffer.BerserkMode(false);

                _soundService.StopCharacterEffectSounds();
                _soundService.StopMoveActionSounds();
                _analyticsService.HandlePlayerDeath(command.DeathReason);

                var soundEntity = _game.SoundEntity;
                PlayHitSound(soundEntity);

                _soundService.PlaySound(ESoundFxName.DeathGroan);

                if (entity.IsDash)
                    _commandBuffer.Dash(false);
            }
            else
            {
                _ghostSoundDispatcher.StopMoveActionSounds(command.CharacterUid);
                _ghostSoundDispatcher.StopCharacterEffectSounds(command.CharacterUid);
            }

            if (entity.HasObstacleFeedbackAfterTimer)
                entity.RemoveObstacleFeedbackAfterTimer();

            _commandBuffer.CancelAcceleration(entity.Uid.Value);

#if !DISABLE_GHOSTS
            if (entity.IsGhost)
            {
                DelayedInvocationService.Invoke(() => _commandBuffer.Revive(entity.Uid.Value), _ghostsParameters.RespawnDelay);
                return;
            }
#endif

            if (command.DeathReason == EDeathReason.OutOfBounds)
            {
                _gameStateService.EnterLose();
                return;
            }

            var delayBeforeFall = _soundSettingsBase.DelayBeforeFallSoundWhenDead;
            DelayedInvocationService.Invoke(() => _soundService.PlaySound(ESoundFxName.FallDown), delayBeforeFall);

            if (isInTutorial)
            {
                _commandBuffer.Revive(command.CharacterUid);
                return;
            }

            if (command.IgnoreHandAppearance)
            {
                _gameStateService.EnterRevivingChoice();
                return;
            }
            
            if (!entity.IsHoleChaserProtection)
            {
                _commandBuffer.ShowMonster();
            }
        }

        private void PlayHitSound(GameEntity sound)
        {
            // Более показать не могу
        }
    }
}