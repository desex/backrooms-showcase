using System;
using System.Collections.Generic;
using Db.SoundFxBase;
using Ecs.Managers;
using Ecs.Utils;
using Game.Services.Sound;
using Game.Utils.Movement;
using Game.Utils.Sound;
using JCMG.EntitasRedux;
using R3;
using Zenject;
#if !DISABLE_GHOSTS
using Ecs.Utils.Groups;
using Game.Services.Ghosts.Sound;
#endif

namespace Game.Services.Player.Impl
{
    public sealed class PlayerMovementTypeService : IPlayerMovementTypeService, IInitializable, IDisposable
    {
        private readonly GameContext _game;
        private readonly ISoundService _soundService;
#if !DISABLE_GHOSTS
        private readonly IGameGroupUtils _gameGroupUtils;
        private readonly IGhostSoundDispatcher _ghostSoundDispatcher;
#endif

        private readonly CompositeDisposable _compositeDisposable = new CompositeDisposable();
        private IReadOnlyDictionary<ESoundFxName, ESoundFxName> _playerToGhostSounds;

        public PlayerMovementTypeService(
            GameContext game,
#if !DISABLE_GHOSTS
            IGameGroupUtils gameGroupUtils,
            IGhostSoundDispatcher ghostSoundDispatcher,
#endif
            ISoundService soundService
        )
        {
            _game = game;
            _soundService = soundService;
#if !DISABLE_GHOSTS
            _gameGroupUtils = gameGroupUtils;
            _ghostSoundDispatcher = ghostSoundDispatcher;
#endif
        }

        public void Initialize()
        {
            _game.OnEntityCreated += OnEntityCreated;

            _playerToGhostSounds = new Dictionary<ESoundFxName, ESoundFxName>
            {
                {
                    ESoundFxName.Run, ESoundFxName.GhostRun
                },
                {
                    ESoundFxName.Somersault, ESoundFxName.GhostSomersault
                },
                {
                    ESoundFxName.RegularJump, ESoundFxName.GhostRegularJump
                },
                {
                    ESoundFxName.Slide, ESoundFxName.GhostSlide
                },
                {
                    ESoundFxName.RunningOnArmstrong, ESoundFxName.GhostRunningOnArmstrong
                },
                {
                    ESoundFxName.TheSoundOfFrictionOfALatexSuit, ESoundFxName.GhostSoundOfFrictionOfALatexSuit
                },
                {
                    ESoundFxName.BreathingWhileRunning, ESoundFxName.GhostBreathingWhileRunning
                },
            };
        }

        public void ChangeBodyMoveType(Uid playerUid, EBodyMoveType movementType)
        {
            var player = _game.GetEntityWithUid(playerUid);

            player.ReplaceBodyMoveType(movementType);
        }

        private void OnEntityCreated(IContext context, IEntity entity)
        {
            if (_game.GameStateEntity == null)
                return;

            _game.OnEntityCreated -= OnEntityCreated;

            _game.GameStateEntity
                .SubscribeGameState(OnGameState)
                .AddTo(_compositeDisposable);
        }

        private void OnGameState(GameEntity entity, EGameState value)
        {
            if (value != EGameState.Game)
                return;

            var player = _game.PlayerEntity;

            if (player == null)
                return;

            UpdateBodyType(player.Uid.Value);

#if !DISABLE_GHOSTS
            using var _ = _gameGroupUtils.GetGhosts(out var ghosts);
            
            foreach (var ghost in ghosts)
                UpdateBodyType(ghost.Uid.Value);
#endif
        }

        public void UpdateBodyType(Uid characterUid)
        {
            if (_game.GameStateEntity == null)
                return;

            var gameState = _game.GameStateEntity.GameState.Value;

            if (gameState != EGameState.Game)
                return;

            var character = _game.GetEntityWithUid(characterUid);

            if (character.IsDead)
                return;

            if (character.IsWatchingOnChaser)
                return;

            if (character.IsFallingInHole)
                return;

            if (character.HasQteType)
            {
                _soundService.StopMoveActionSounds();
                return;
            }
            
            if (character.IsPlayer)
                _soundService.StopMoveActionSounds();
#if !DISABLE_GHOSTS
            else
                _ghostSoundDispatcher.StopMoveActionSounds(characterUid);
#endif

            if (TryApplyDashInHole(character))
                return;

            if (TryApplyComebackInHole(character))
                return;

            if (TryApplyWallRun(character))
                return;

            if (TryApplyWallJump(character))
                return;

            if (TryApplyClimb(character))
                return;

            if (TryApplyJump(character))
                return;

            if (TryApplySliding(character))
                return;

            if (TryInterruptFall(character))
                return;

            if (TryApplyLand(character))
                return;

            ApplyRun(character);
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }

        private static bool TryApplyComebackInHole(GameEntity character)
        {
            if (!character.IsComebackInHole)
                return false;

            character.ReplaceBodyMoveType(EBodyMoveType.ComebackInHole);

            return true;
        }

        private static bool TryApplyDashInHole(GameEntity character)
        {
            if (!character.IsDashInHole)
                return false;

            character.ReplaceBodyMoveType(EBodyMoveType.DashInHole);

            return true;
        }

        private bool TryApplyClimb(GameEntity character)
        {
            if (!character.IsClimbing)
                return false;

            character.ReplaceBodyMoveType(EBodyMoveType.Climb);
            PlaySound(character, ESoundFxName.RegularJump);

            return true;
        }

        private bool TryApplySliding(GameEntity character)
        {
            if (!character.HasSliding)
                return false;

            switch (character.Sliding.Type)
            {
                case ESlideType.Short:
                    character.ReplaceBodyMoveType(EBodyMoveType.ShortSlide);
                    PlaySound(character, ESoundFxName.Slide);
                    break;

                case ESlideType.Controllable:
                    character.ReplaceBodyMoveType(EBodyMoveType.ControllableSlide);
                    break;

                default:
                    return false;
            }

            return true;
        }

        private bool TryApplyJump(GameEntity character)
        {
            if (!character.HasJumping)
                return false;

            PlaySound(character, ESoundFxName.RegularJump);

            switch (character.Jumping.Value)
            {
                case EJumpType.ForwardJump:
                    character.ReplaceBodyMoveType(EBodyMoveType.ForwardJump);
                    return true;

                case EJumpType.JumpInObstacle:
                    character.ReplaceBodyMoveType(EBodyMoveType.JumpInObstacle);
                    return true;

                case EJumpType.JumpInObstacleCut:
                    character.ReplaceBodyMoveType(EBodyMoveType.JumpInObstacleCut);
                    return true;

                case EJumpType.MonkeyJump:
                    character.ReplaceBodyMoveType(EBodyMoveType.MonkeyJump);
                    return true;

                case EJumpType.JumpOn:
                    character.ReplaceBodyMoveType(EBodyMoveType.JumpOn);
                    return true;

                case EJumpType.Swing:
                    character.ReplaceBodyMoveType(EBodyMoveType.Swing);
                    return true;

                case EJumpType.Jump:
                case EJumpType.DoubleJump:
                case EJumpType.JumpOverObstacle:
                    character.ReplaceBodyMoveType(EBodyMoveType.Jump);
                    return true;

                default:
                    return false;
            }
        }

        private static bool TryInterruptFall(GameEntity character)
        {
            if (!character.IsFallInterrupted || character.BodyMoveType.Value == EBodyMoveType.InterruptFall)
                return false;

            character.ReplaceBodyMoveType(EBodyMoveType.InterruptFall);

            return true;
        }

        private bool TryApplyLand(GameEntity character)
        {
            if (!character.IsFallInterrupted || character.BodyMoveType.Value != EBodyMoveType.InterruptFall)
                return false;

            PlaySound(character, ESoundFxName.Somersault);
            character.ReplaceBodyMoveType(EBodyMoveType.Landing);

            return true;
        }

        private static bool TryApplyWallJump(GameEntity character)
        {
            if (!character.HasCharacterTouchWall)
                return false;

            var isDefaultJump = character.HasJumping && character.Jumping.Value == EJumpType.Jump;

            if (!isDefaultJump)
                return false;

            character.ReplaceBodyMoveType(EBodyMoveType.Jump);

            return true;
        }

        private bool TryApplyWallRun(GameEntity character)
        {
            if (!character.HasCharacterTouchWall || !character.HasWallRun)
                return false;

            PlaySound(character, ESoundFxName.Run);
            character.ReplaceBodyMoveType(EBodyMoveType.WallRun);

            return true;
        }

        private void ApplyRun(GameEntity character)
        {
            if (character.IsHitOnObstacle)
                return;

            var soundEntity = _game.SoundEntity;

            if (!soundEntity.HasSurfaceInteractionType)
                PlayStandardRunSound();
            else
                PlaySpecialRunSound();

            character.ReplaceBodyMoveType(EBodyMoveType.Run);

            return;

            void PlaySpecialRunSound()
            {
                switch (soundEntity.SurfaceInteractionType.Value)
                {
                    case ESurfaceInteractionType.Armstrong:
                        PlaySound(character, ESoundFxName.RunningOnArmstrong);
                        PlayAdditionalRunSounds();
                        break;
                }
            }

            void PlayStandardRunSound()
            {
                PlaySound(character, ESoundFxName.Run);
                PlayAdditionalRunSounds();
            }

            void PlayAdditionalRunSounds()
            {
                PlaySound(character, ESoundFxName.TheSoundOfFrictionOfALatexSuit);
                PlaySound(character, ESoundFxName.BreathingWhileRunning);
            }
        }

        private void PlaySound(GameEntity character, ESoundFxName regularSound)
        {
            var characterUid = character.Uid.Value;

#if !DISABLE_GHOSTS
            if (character.IsGhost)
                _ghostSoundDispatcher.PlaySound(characterUid, _playerToGhostSounds[regularSound]);
#endif

            _soundService.PlaySound(regularSound);
        }
    }
}