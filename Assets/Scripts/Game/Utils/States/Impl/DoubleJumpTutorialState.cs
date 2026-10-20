using System;
using Cysharp.Threading.Tasks;
using Db.Tutorial;
using Game.Services.Hint;
using Game.Services.Player;
using Game.Services.TimeScale;
using Game.Services.Tutorial;
using Game.Utils.ColorsUtils;
using Game.Utils.Movement;
using Game.Utils.MovementBlock;
using Game.Utils.Tutorial;
using Game.Utils.UniTaskExtensions;
using JCMG.EntitasRedux.Commands;
using R3;
using UnityEngine;

namespace Game.Utils.States.Impl
{
    public class DoubleJumpTutorialState : ATutorialState
    {
        private bool _isNextHintShowed;
        
        public DoubleJumpTutorialState(
            ITutorialParameters parameters,
            GameContext game,
            ITimeScaleService timeScaleService,
            ICommandBuffer commandBuffer,
            ETutorialState stateType,
            ITutorialHintService tutorialHintService,
            IPlayerMovementTypeService playerMovementTypeService,
            ITutorialService tutorialService
        ) : base(parameters,
                 game,
                 timeScaleService,
                 commandBuffer,
                 stateType,
                 tutorialHintService,
                 playerMovementTypeService,
                 tutorialService
        )
        {
        }

        protected override void OnEnter()
        {
            DebugUtility.Log($"Entered {nameof(DoubleJumpTutorialState)}", UtilsColors.NotificationColor);

            RemovePassObstaclesComponents();

            var player = Game.PlayerEntity;

            if (player.IsTutorialFallingInHole)
                player.IsTutorialFallingInHole = false;

            PresentSimpleJump(player, true);
            Observable.EveryUpdate().Subscribe(_ => CheckDistances()).AddTo(CommonDisposable);
        }

        protected override void OnRestart()
        {
            RemovePassObstaclesComponents();
            _isNextHintShowed = false;
        }

        protected override void OnExit()
        {
            DebugUtility.Log($"Exited {nameof(DoubleJumpTutorialState)}", UtilsColors.NotificationColor);
        }

        private void PresentSimpleJump(GameEntity player, bool needSlowdown)
        {
            StartMovementPresentation(EBlockedMovement.Jump, ETutorialHint.Jump, needSlowdown);
            player.SubscribeJumping(FinishPresentationAndStartDoubleJumpOnJumping).AddTo(ActionDisposable);

            return;

            void FinishPresentationAndStartDoubleJumpOnJumping(GameEntity entity, EJumpType jumpType)
            {
                if (jumpType is not EJumpType.Jump)
                    return;

                FinishActionPresentation(EBlockedMovement.Jump, ETutorialHint.Jump, needSlowdown);
                PresentDoubleJump(entity, needSlowdown).ForgetAndWrapExceptions();
            }
        }

        private async UniTask PresentDoubleJump(GameEntity player, bool needSlowdown)
        {
            var doubleJumpPresentationDelayAfterSingleJump =
                Parameters.DoubleJumpParameters.DoubleJumpPresentationDelayAfterSingleJump;
            await UniTask.Delay(TimeSpan.FromSeconds(doubleJumpPresentationDelayAfterSingleJump));

            StartMovementPresentation(EBlockedMovement.DoubleJump, ETutorialHint.DoubleJump, needSlowdown);
            player.SubscribeJumping(FinishPresentationOnJumping).AddTo(ActionDisposable);
            player.SubscribeGrounded(_ => RestartWithTeleport()).AddTo(ActionDisposable);

            return;

            void FinishPresentationOnJumping(GameEntity entity, EJumpType jumpType)
            {
                if (jumpType is not EJumpType.DoubleJump)
                    return;

                FinishActionPresentation(EBlockedMovement.DoubleJump, ETutorialHint.DoubleJump, needSlowdown);
            }
        }

        private void RemovePassObstaclesComponents()
        {
            var player = Game.PlayerEntity;

            if (player.HasReadyToObstaclePass)
                player.RemoveReadyToObstaclePass();

            if (player.HasDashWhenInObstacle)
                player.RemoveDashWhenInObstacle();

            if (player.HasPassObstacleMovePoints)
                player.RemovePassObstacleMovePoints();
        }
        
        private void CheckDistances()
        {
            if (!CanShowNexDoubleJumpHint())
                return;
            
            if (_isNextHintShowed)
                return;
            
            _isNextHintShowed = true;
            PresentNextDoubleJump(Game.PlayerEntity);
            Game.PlayerEntity.SubscribeTutorialFallingInHole(FinishNextDoubleJump).AddTo(CommonDisposable);
        }

        private void FinishNextDoubleJump(GameEntity entity)
        {
            var player = Game.PlayerEntity;

            if (player.BodyMoveType.Value == EBodyMoveType.Run)
                FinishActionPresentation(EBlockedMovement.Jump, ETutorialHint.Jump, false);

            if (player.HasJumping && player.Jumping.Value == EJumpType.Jump)
                FinishActionPresentation(EBlockedMovement.DoubleJump, ETutorialHint.DoubleJump, false);
        }
        
        private void PresentNextDoubleJump(GameEntity player)
        {
            StartMovementPresentation(EBlockedMovement.Jump, ETutorialHint.Jump, false);
            player.SubscribeJumping(PresentNextOnJumping).AddTo(ActionDisposable);

            return;

            void PresentNextOnJumping(GameEntity entity, EJumpType jumpType)
            {
                if (jumpType is not EJumpType.Jump)
                    return;
                
                FinishActionPresentation(EBlockedMovement.Jump, ETutorialHint.Jump, false);
                PresentNextDoubleJumpSecondPart(entity);
            }
        }

        private void PresentNextDoubleJumpSecondPart(GameEntity player)
        {
            StartMovementPresentation(EBlockedMovement.DoubleJump, ETutorialHint.DoubleJump, false);
            
            player.SubscribeJumping(FinishPresentationOnJumping).AddTo(ActionDisposable);
            Observable
               .TimerFrame(3)
               .Subscribe(_ => player.SubscribeGrounded(_ => RestartWithTeleport()).AddTo(ActionDisposable))
               .AddTo(CommonDisposable);

            return;

            void FinishPresentationOnJumping(GameEntity entity, EJumpType jumpType)
            {
                if (jumpType is not EJumpType.DoubleJump)
                    return;

                FinishActionPresentation(EBlockedMovement.DoubleJump, ETutorialHint.DoubleJump, false);
            }
        }

        private bool CanShowNexDoubleJumpHint()
        {
            var player = Game.PlayerEntity;
            var playerPosition = player.Position.Value;
            var chunkPosition = Game.GetEntityWithUid(player.CurrentChunkId.Value).ChunkAnchors.Value.StartAnchor.position;
            var vectorFromChunkStartAnchor = chunkPosition - playerPosition;
            var playerForward = player.Transform.Value.forward;
            var vectorFromChunkStartBorder = Vector3.Project(vectorFromChunkStartAnchor, playerForward);
            var passedSqrDistance = vectorFromChunkStartBorder.sqrMagnitude;
            
            return passedSqrDistance >= Mathf.Pow(Parameters.DoubleJumpParameters.SecondDoubleJumpHintShowDistance, 2);
        }
    }
}