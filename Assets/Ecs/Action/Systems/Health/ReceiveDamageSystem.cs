using Db.Effects;
using Db.Player;
using Db.SoundFxBase;
using Ecs.Action.Commands;
using Ecs.Utils;
using Game.Services.Ghosts.Sound;
using Game.Services.Sound;
using Game.UI.Effects;
using Game.Utils.Sound;
using Generated.Commands;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;
using UnityEngine;

namespace Ecs.Action.Systems.Health
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 940, nameof(EFeatures.Move))]
    public class ReceiveDamageSystem : ForEachCommandUpdateSystem<ReceiveDamageCommand>
    {
        private readonly ICommandBuffer _commandBuffer;
        private readonly GameContext _game;
        private readonly IGhostSoundDispatcher _ghostSoundDispatcher;
        private readonly IPlayerHealthParametersBase _playerHealthParametersBase;
        private readonly ISoundService _soundService;
        private readonly IUiEffectController _uiEffectController;

        public ReceiveDamageSystem(
            ICommandBuffer commandBuffer,
            GameContext game,
            IPlayerHealthParametersBase playerHealthParametersBase,
            ISoundService soundService,
            IGhostSoundDispatcher ghostSoundDispatcher,
            IUiEffectController uiEffectController
        ) : base(commandBuffer)
        {
            _commandBuffer = commandBuffer;
            _game = game;
            _playerHealthParametersBase = playerHealthParametersBase;
            _soundService = soundService;
            _ghostSoundDispatcher = ghostSoundDispatcher;
            _uiEffectController = uiEffectController;
        }

        protected override void Execute(ref ReceiveDamageCommand command)
        {
            var tutorialEntity = _game.TutorialEntity;
            var isTutorialActive = tutorialEntity.IsInTutorial;

            if (isTutorialActive)
                return;

            var character = _game.GetEntityWithUid(command.CharacterUid);

            if (character.IsDead)
                return;

            if (character.HasDamageCooldown)
                return;

            if (!character.IsCanBeDamaged)
                return;

            if (character.IsCanBeInvulnerable)
                return;

            character.IsObstaclePassCondition = false;

            if (character.HasObstacleFeedbackAfterTimer)
                character.RemoveObstacleFeedbackAfterTimer();

            _commandBuffer.CancelAcceleration(command.CharacterUid);

            var soundEntity = _game.SoundEntity;
            PlayHitSound(character, soundEntity);

            if (character.IsPlayer)
                _uiEffectController.PlayBloodEffectAnimation(EUiEffectType.BloodEffect);

            PlaySound(character, ESoundFxName.ObstacleCollisionGroan);
            PlaySound(character, ESoundFxName.WalkingDuringRecovery);

            character.IsDamaged = true;
            character.IsDamagedVisually = true;

            UpdateHitDirection(command.HitDirection);

            EnableDamageCooldown(character);
            RegeneratePlayerHealthAfterDamage(character);
            StartDamageVisualsTimer(character);
            SetPlayerInvulnerability(character);
            RemoveChaserAfterQteSlowdown();

            return;

            void UpdateHitDirection(Vector3 hitDirection)
            {
                var sideAngleThreshold = _playerHealthParametersBase.HitObstacleSideAngleThreshold;

                var forward = character.Transform.Value.forward;
                var sideAngle = Vector3.SignedAngle(forward, -hitDirection, Vector3.up);
                var absSideAngle = Mathf.Abs(sideAngle);

                EHitDirection direction;

                if (absSideAngle <= sideAngleThreshold)
                    direction = EHitDirection.Forward;
                else
                    direction = sideAngle > 0 ? EHitDirection.Right : EHitDirection.Left;

                character.ReplaceHitDirection(direction);
            }
        }

        private void SetPlayerInvulnerability(GameEntity player)
        {
            player.IsCanBeInvulnerable = true;
            _commandBuffer.SetPlayerInvulnerable(player.Uid.Value);
        }

        private void EnableDamageCooldown(GameEntity player)
        {
            player.ReplaceDamageCooldown(_playerHealthParametersBase.TimeBetweenDamages);
        }

        private void RegeneratePlayerHealthAfterDamage(GameEntity character)
        {
            var timeBetweenInjuries = _playerHealthParametersBase.TimeBetweenInjuries;
            character.ReplaceRegenerationTimer(timeBetweenInjuries);
        }

        private void StartDamageVisualsTimer(GameEntity character)
        {
            var timeBeforeRemoveDamageVisuals = _playerHealthParametersBase.TimeBeforeRemoveDamageVisuals;
            character.ReplaceDamageVisualsTimer(timeBeforeRemoveDamageVisuals);
        }

        private void PlayHitSound(GameEntity character, GameEntity sound)
        {
            if (!sound.HasSurfaceInteractionType)
            {
                PlaySound(character, ESoundFxName.HitWall);
                return;
            }

            var type = sound.SurfaceInteractionType.Value;

            var soundFxName = type switch
            {
                ESurfaceInteractionType.WoodHit => ESoundFxName.HitWithWoodSurface,
                ESurfaceInteractionType.MetalHit => ESoundFxName.HitWithMetalSurface,
                _ => ESoundFxName.None,
            };

            if (soundFxName is not ESoundFxName.None)
                PlaySound(character, soundFxName);
        }

        private void PlaySound(GameEntity characterEntity, ESoundFxName soundFxName)
        {
            if (characterEntity.IsPlayer)
                _soundService.PlaySound(soundFxName);
            else
                _ghostSoundDispatcher.PlaySound(characterEntity.Uid.Value, soundFxName);
        }

        private void RemoveChaserAfterQteSlowdown()
        {
            var chaser = _game.ChaserEntity;
            chaser.IsInAfterQteSlowdown = false;
        }
    }
}