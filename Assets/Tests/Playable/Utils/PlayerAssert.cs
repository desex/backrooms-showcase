using System;
using System.Collections;
using System.Linq;
using Db.Score;
using Game.Services.Score;
using Game.UI.Combo;
using Game.Utils.Movement;
using Game.Utils.Obstacles;
using NUnit.Framework;
using Tests.Playable.UITest.Custom.Conditions.Entity;
using Tests.Playable.UITest.Utils;
using UnityEngine;
using static System.String;
using Object = UnityEngine.Object;

namespace Tests.Playable.Utils
{
    public static class PlayerAssert
    {
        public static IEnumerator IsNotSetTargetRotation(float timeout = 10f)
        {
            yield return null;
            
            yield return new EntityMatchCase<GameEntity>.Builder()
                .SetConditionName("Player is not set target rotation")
                .SetEntityFilter(entity => entity.IsPlayer && 
                                           entity.HasTargetRotation == false && 
                                           entity.IsDestroyed == false)
                .SetCount(1)
                .SetTimeout(timeout)
                .Build();
            
            yield return null;
        }   
        
        public static IEnumerator IsReadyToComeback(float timeout = 10f)
        {
            yield return null;
            
            yield return new EntityMatchCase<GameEntity>.Builder()
                        .SetConditionName("Player is not set dash trigger")
                        .SetEntityFilter(entity => entity.IsPlayer && 
                                                   entity.HasHoleDashTrigger && 
                                                   entity.IsDestroyed == false)
                        .SetCount(1)
                        .SetTimeout(timeout)
                        .Build();
            
            yield return null;
        }    
        
        public static IEnumerator IsSetTargetRotation(float timeout = 10f)
        {
            yield return null;
            
            yield return new EntityMatchCase<GameEntity>.Builder()
                .SetConditionName("Player is not set target rotation")
                .SetEntityFilter(entity => entity.IsPlayer && 
                                           entity.HasTargetRotation && 
                                           entity.IsDestroyed == false)
                .SetCount(1)
                .SetTimeout(timeout)
                .Build();
            
            yield return null;
        }
        
        public static IEnumerator IsGrounded()
        {
            yield return null;
            
            yield return new EntityMatchCase<GameEntity>.Builder()
                .SetConditionName("Player jumping")
                .SetEntityFilter(entity => entity.IsPlayer && entity.CharacterController.Value.isGrounded && !entity.IsDestroyed)
                .SetCount(1)
                .SetTimeout(10)
                .Build();
            
            yield return null;
        }       
        
        public static IEnumerator IsNotGrounded()
        {
            yield return null;
            
            yield return new EntityMatchCase<GameEntity>.Builder()
                .SetConditionName("Player jumping")
                .SetEntityFilter(entity => entity.IsPlayer && !entity.CharacterController.Value.isGrounded && !entity.IsDestroyed)
                .SetCount(1)
                .SetTimeout(10)
                .Build();
            
            yield return null;
        }
        
        public static IEnumerator IsRunningUpSurface()
        {
            yield return null;
            
            yield return new EntityMatchCase<GameEntity>.Builder()
                        .SetConditionName("Player is moving up surface")
                        .SetEntityFilter(entity => entity.IsPlayer && entity.IsMovingUpSurface && !entity.IsDestroyed)
                        .SetCount(1)
                        .SetTimeout(2)
                        .Build();
            
            yield return null;
        }
        
        public static IEnumerator IsNotRunningUpSurface()
        {
            yield return null;
            
            yield return new EntityMatchCase<GameEntity>.Builder()
                        .SetConditionName("Player is not moving up surface")
                        .SetEntityFilter(entity => entity.IsPlayer && !entity.IsMovingUpSurface && !entity.IsDestroyed)
                        .SetCount(1)
                        .SetTimeout(2)
                        .Build();
            
            yield return null;
        }
        
        public static IEnumerator IsGroundedDuringTime(float checkingDuration)
        {
            yield return null;
            
            yield return new EntityMatchCase<GameEntity>.Builder()
                        .SetConditionName("Player is grounded")
                        .SetEntityFilter(entity => entity.IsPlayer && entity.IsGrounded && !entity.IsDestroyed)
                        .SetCount(1)
                        .SetTimeout(checkingDuration)
                        .SetContinuousChecking(true)
                        .Build();
            
            yield return null;
        }
        
        public static IEnumerator ForwardSpeedMultiplier(float equals, float timeout = 1f)
        {
            yield return null;
            
            yield return new EntityMatchCase<GameEntity>.Builder()
                        .SetConditionName($"Player forward speed is {equals}")
                        .SetEntityFilter(entity => entity.IsPlayer && entity.HasForwardSpeedMultiplier && Math.Abs(entity.ForwardSpeedMultiplier.Value - equals) < 10e-5 && !entity.IsDestroyed)
                        .SetCount(1)
                        .SetTimeout(timeout)
                        .Build();
            
            yield return null;
        }

        public static IEnumerator DistancePassed(int equals, float timeout = 10f)
        {
            yield return null;
            
            yield return new EntityMatchCase<GameEntity>.Builder()
                        .SetConditionName($"Player forward speed is {equals}")
                        .SetEntityFilter(entity => entity.IsPlayer && entity.HasPassedDistanceInMeters && entity.PassedDistanceInMeters.Value == equals && !entity.IsDestroyed)
                        .SetCount(1)
                        .SetTimeout(timeout)
                        .Build();
            
            yield return null;
        }

        public static IEnumerator IsDamaged(float timeout = 10)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                .SetConditionName("Player get damage")
                .SetEntityFilter(entity => entity.IsPlayer && entity.IsDamaged && !entity.IsDestroyed)
                .SetCount(1)
                .SetTimeout(timeout)
                .Build();
        }

        public static IEnumerator IsNotDamaged(float timeout = 10)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                .SetConditionName("Player get damage")
                .SetEntityFilter(entity => entity.IsPlayer && !entity.IsDamaged && !entity.IsDestroyed)
                .SetCount(1)
                .SetTimeout(timeout)
                .Build();
        }
        
        public static IEnumerator IsDead(float timeOut = 10f)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                .SetConditionName("Player is dead")
                .SetEntityFilter(entity => entity.IsPlayer 
                                           && entity.IsDead 
                                           && !entity.IsDestroyed)
                .SetCount(1)
                .SetTimeout(timeOut)
                .Build();
        }
        
        public static IEnumerator IsNotDead(float timeout = 10f)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                .SetConditionName("Player is not dead")
                .SetEntityFilter(entity => entity.IsPlayer 
                                           && !entity.IsDead 
                                           && !entity.IsDestroyed)
                .SetCount(1)
                .SetTimeout(timeout)
                .Build();
        }
        
        public static IEnumerator BodyMoveType(EBodyMoveType bodyMoveType, float timeout = 10)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                .SetConditionName($"Player body move type is {bodyMoveType}")
                .SetEntityFilter(entity => entity.IsPlayer && 
                                           entity.HasBodyMoveType && 
                                           entity.BodyMoveType.Value == bodyMoveType && 
                                           !entity.IsDestroyed)
                .SetCount(1)
                .SetTimeout(timeout)
                .Build();
        }
        
        public static IEnumerator NotBodyMoveType(EBodyMoveType bodyMoveType, float timeout = 10)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                        .SetConditionName("Player not slide down")
                        .SetEntityFilter(entity => entity.IsPlayer && 
                                                   entity.HasBodyMoveType && 
                                                   entity.BodyMoveType.Value != bodyMoveType && 
                                                   !entity.IsDestroyed)
                        .SetCount(1)
                        .SetTimeout(timeout)
                        .Build();
            
            yield return null;
        }
        
        public static IEnumerator IsReadyToWallRun(float timeout = 10)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                        .SetConditionName("Player is ready to wall run")
                        .SetEntityFilter(entity => entity.IsPlayer &&
                                                   entity.HasCharacterTouchWall &&
                                                   !entity.IsDestroyed)
                        .SetCount(1)
                        .SetTimeout(timeout)
                        .Build();
        }
        
        public static IEnumerator IsNotReadyToWallRun(float timeout = 10)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                        .SetConditionName("Player is not ready to wall run")
                        .SetEntityFilter(entity => entity.IsPlayer && 
                                                   !entity.HasCharacterTouchWall &&
                                                   !entity.IsDestroyed)
                        .SetCount(1)
                        .SetTimeout(timeout)
                        .Build();
        }
        public static IEnumerator IsReadyToJumpOverObstacle(float timeout = 10)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                .SetConditionName("Player is ready to jump over obstacle")
                .SetEntityFilter(entity => entity.IsPlayer && 
                                           entity.IsReadyForJumpOverObstacles && 
                                           !entity.IsDestroyed)
                .SetCount(1)
                .SetTimeout(timeout)
                .Build();
        }
        
        public static IEnumerator IsNotReadyToJumpOverObstacle(float timeout = 10)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                .SetConditionName("Player is not ready to jump over obstacle")
                .SetEntityFilter(entity => entity.IsPlayer && 
                                           !entity.IsReadyForJumpOverObstacles && 
                                           !entity.IsDestroyed)
                .SetCount(1)
                .SetTimeout(timeout)
                .Build();
        }
        
        public static IEnumerator IsReadyToClimb(float timeout = 10)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                .SetConditionName("Player is ready to climb over obstacle")
                .SetEntityFilter(entity => entity.IsPlayer && 
                                           entity.HasReadyToClimb && 
                                           !entity.IsDestroyed)
                .SetCount(1)
                .SetTimeout(timeout)
                .Build();
        }
        
        public static IEnumerator IsNotReadyToClimb(float timeout = 10)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                .SetConditionName("Player is not ready to Climb over obstacle")
                .SetEntityFilter(entity => entity.IsPlayer && 
                                           !entity.HasReadyToClimb && 
                                           !entity.IsDestroyed)
                .SetCount(1)
                .SetTimeout(timeout)
                .Build();
        }
        
        public static IEnumerator IsReadyToFailFall(float timeout = 10)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                        .SetConditionName("Player is ready to take fall damage")
                        .SetEntityFilter(entity => entity.IsPlayer && 
                                                   entity.IsReadyToFailFall && 
                                                   !entity.IsDestroyed)
                        .SetCount(1)
                        .SetTimeout(timeout)
                        .Build();
        }
        
        public static IEnumerator IsNotReadyToFailFall(float timeout = 10)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                        .SetConditionName("Player is not ready to take fall damage")
                        .SetEntityFilter(entity => entity.IsPlayer && 
                                                   !entity.IsReadyToFailFall && 
                                                   !entity.IsDestroyed)
                        .SetCount(1)
                        .SetTimeout(timeout)
                        .Build();
        }

        public static IEnumerator IsReadyForFastActionMove(float timeout = 10)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                        .SetConditionName("Is ready for fast action move")
                        .SetEntityFilter(entity => entity.IsPlayer && entity.IsReadyForFastActionMove && !entity.IsDestroyed)
                        .SetCount(1)
                        .SetTimeout(timeout)
                        .Build();
        }
        
        public static IEnumerator Jumping(EJumpType jumpType, float timeout = 1f)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                        .SetConditionName($"Is in {jumpType}")
                        .SetEntityFilter(entity => entity.IsPlayer
                                                && entity.HasJumping
                                                && entity.Jumping.Value == jumpType
                                                && !entity.IsDestroyed
                         )
                        .SetCount(1)
                        .SetTimeout(timeout)
                        .Build();
        }
        
        public static IEnumerator NotJumping(float timeout = 1f)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                        .SetConditionName("Is not jumping")
                        .SetEntityFilter(entity => entity.IsPlayer
                                                && !entity.HasJumping
                                                && !entity.IsDestroyed
                         )
                        .SetCount(1)
                        .SetTimeout(timeout)
                        .Build();
        }
        
        public static IEnumerator IsNotReadyForFastActionMove(float timeout = 10)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                        .SetConditionName("Is not ready for fast action move")
                        .SetEntityFilter(entity => entity.IsPlayer && !entity.IsReadyForFastActionMove && !entity.IsDestroyed)
                        .SetCount(1)
                        .SetTimeout(timeout)
                        .Build();
        }

        public static IEnumerator IsInFastActionMove(float timeout = 10)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                        .SetConditionName("Is in fast action move")
                        .SetEntityFilter(entity => entity.IsPlayer && entity.IsFastActionMove && !entity.IsDestroyed)
                        .SetCount(1)
                        .SetTimeout(timeout)
                        .Build();
        }
        
        public static IEnumerator IsNotInFastActionMove(float timeout = 10)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                        .SetConditionName("Is not in fast action move")
                        .SetEntityFilter(entity => entity.IsPlayer && !entity.IsFastActionMove && !entity.IsDestroyed)
                        .SetCount(1)
                        .SetTimeout(timeout)
                        .Build();
        }

        public static IEnumerator PlayerRun()
        {
            yield return null;
            
            yield return new EntityMatchCase<GameEntity>.Builder()
                .SetConditionName("player is running")
                .SetEntityFilter(entity => entity.IsPlayer && entity.HasVelocity && entity.Velocity.Value != Vector3.zero && !entity.IsDestroyed)
                .SetCount(1)
                .SetTimeout(10)
                .Build();

            yield return null;
        }
        
        public static IEnumerator IsInterrupted()
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                .SetConditionName("Jump is interrupted")
                .SetEntityFilter(entity => entity.IsPlayer
                                           && entity.HasBodyMoveType 
                                           && entity.BodyMoveType.Value == EBodyMoveType.InterruptFall
                                           && !entity.IsDestroyed)
                .SetCount(1)
                .SetTimeout(10)
                .Build();
        }
        
        public static IEnumerator IsInjured()
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                .SetConditionName("Player is injured")
                .SetEntityFilter(entity => entity.IsPlayer 
                                           && entity.IsDamaged
                                           && !entity.IsDestroyed)
                .SetCount(1)
                .SetTimeout(10)
                .Build();
        }

        public static IEnumerator IsInDamageRebound()
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                .SetConditionName("Player is in damage rebound")
                .SetEntityFilter(entity => entity.IsPlayer 
                                        && entity.HasDamageReboundDirection
                                        && !entity.IsDestroyed)
                .SetCount(1)
                .SetTimeout(2)
                .Build();
        }

        public static IEnumerator IsNotInDamageRebound()
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                .SetConditionName("Player is not in damage rebound")
                .SetEntityFilter(entity => entity.IsPlayer
                                        && !entity.HasDamageReboundDirection
                                        && !entity.IsDestroyed
                 )
                .SetCount(1)
                .SetTimeout(3)
                .Build();
        }

        public static IEnumerator IsReadyToJumpOn(float timeout = 10)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                .SetConditionName("Player is ready to jump on")
                .SetEntityFilter(entity => entity.IsPlayer && 
                                           entity.HasReadyToJumpOn && 
                                           !entity.IsDestroyed)
                .SetCount(1)
                .SetTimeout(timeout)
                .Build();
        }
        
        public static IEnumerator IsNotReadyToJumpOn(float timeout = 10)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                        .SetConditionName("Player is not ready to jump on")
                        .SetEntityFilter(entity => entity.IsPlayer && 
                                                   !entity.HasReadyToJumpOn && 
                                                   !entity.IsDestroyed)
                        .SetCount(1)
                        .SetTimeout(timeout)
                        .Build();
        }   
        
        public static IEnumerator IsNotJumping(float timeout = 10)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                        .SetConditionName("Player jumping")
                        .SetEntityFilter(entity => entity.IsPlayer && 
                                                   !entity.HasJumping && 
                                                   !entity.IsDestroyed)
                        .SetCount(1)
                        .SetTimeout(timeout)
                        .Build();
        }

        public static IEnumerator MovingHorizontally(bool isMovingHorizontally, float timeout = 2)
        {
            const double tolerance = 0.05f;

            var notState = isMovingHorizontally ? Empty : "not ";
            yield return new EntityMatchCase<GameEntity>.Builder()
                        .SetConditionName($"Player is {notState}moving horizontally")
                        .SetEntityFilter(entity => entity.IsPlayer
                                                && entity.HasHorizontalSpeed
                                                && Math.Abs(entity.Velocity.Value.x) > tolerance == isMovingHorizontally
                                                && !entity.IsDestroyed
                         )
                        .SetCount(1)
                        .SetTimeout(timeout)
                        .Build();
        }

        public static IEnumerator IsPlankRunStart(float timeout = 10)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                .SetConditionName("Plank run start")
                .SetEntityFilter(entity => entity.IsPlayer && 
                                           entity.HasPlankRun && 
                                           !entity.IsDestroyed)
                .SetCount(1)
                .SetTimeout(timeout)
                .Build();
        }

        public static IEnumerator IsPlankRunInProgress(float timeout = 10)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                .SetConditionName("Plank run start")
                .SetEntityFilter(entity => entity.IsPlayer && 
                                           entity.HasPlankRun && 
                                           !entity.IsDestroyed)
                .SetCount(1)
                .SetTimeout(timeout)
                .Build();
        }
        
        public static IEnumerator IsPlankRunEnd(float timeout = 10)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                .SetConditionName("Plank run end")
                .SetEntityFilter(entity => entity.IsPlayer && 
                                           !entity.HasPlankRun && 
                                           !entity.IsDestroyed)
                .SetCount(1)
                .SetTimeout(timeout)
                .Build();
        }

        public static IEnumerator IsReadyToTurn(float timeout = 10)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                        .SetConditionName("Player is ready to turn")
                        .SetEntityFilter(entity => entity.IsPlayer && 
                                                   entity.IsReadyToTurn && 
                                                   !entity.IsDestroyed)
                        .SetCount(1)
                        .SetTimeout(timeout)
                        .Build();
        }
        
        public static IEnumerator IsNotReadyToTurn(float timeout = 10)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                        .SetConditionName("Player is not ready to turn")
                        .SetEntityFilter(entity => entity.IsPlayer && 
                                                   !entity.IsReadyToTurn && 
                                                   !entity.IsDestroyed)
                        .SetCount(1)
                        .SetTimeout(timeout)
                        .Build();
        }

        public static IEnumerator IsSpeedAccelerate(float timeout = 10)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                        .SetConditionName("Player speed accelerated")
                        .SetEntityFilter(entity => entity.IsPlayer && 
                                                   entity.HasSpeedAcceleration && 
                                                   !entity.IsDestroyed)
                        .SetCount(1)
                        .SetTimeout(timeout)
                        .Build();
        }
        
        public static IEnumerator IsSpeedNotAccelerate(float timeout = 10)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                        .SetConditionName("Player speed not accelerated")
                        .SetEntityFilter(entity => entity.IsPlayer && 
                                                   !entity.HasSpeedAcceleration && 
                                                   !entity.IsDestroyed)
                        .SetCount(1)
                        .SetTimeout(timeout)
                        .Build();
        }
        
        public static IEnumerator IsMoveAlongZ(bool isMoveAlongZ, float timeout = 10)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                .SetConditionName("Player is move along Z")
                .SetEntityFilter(entity => entity.IsPlayer && 
                                           entity.ChunkCenter.Value.IsMoveAlongZ == isMoveAlongZ &&
                                           !entity.IsDestroyed)
                .SetCount(1)
                .SetTimeout(timeout)
                .Build();
        }

        public static IEnumerator HasSlideDuration(bool expected, float timeout = 1f)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                        .SetConditionName("Is in monkey jump")
                        .SetEntityFilter(entity => entity.IsPlayer
                                                && entity.HasSlideDuration == expected
                                                && !entity.IsDestroyed
                         )
                        .SetCount(1)
                        .SetTimeout(timeout)
                        .Build();
        }

        public static void ComboMultiplier(float expectedComboMultiplier)
        {
            var scoreService = DiUtils.Container.Resolve<IScoreService>();
            var actualComboMultiplier = scoreService.Combo.CurrentValue;
            
            Assert.AreEqual(expectedComboMultiplier, actualComboMultiplier);
        }
        
        public static void Score(int expectedScore)
        {
            var scoreService = DiUtils.Container.Resolve<IScoreService>();
            var actionsScore = scoreService.CurrentScore.CurrentValue;
            
            Assert.AreEqual(expectedScore, actionsScore);
        }
        
        public static IEnumerator SlowingDownRatio(float slowingDownRatio, float tolerance = 0.01f)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                        .SetConditionName($"Slowing down ratio: {slowingDownRatio}")
                        .SetEntityFilter(entity => entity.IsPlayer
                                                && Math.Abs((entity.HasSlowDownRatio ? entity.SlowDownRatio.Value : 0f) - slowingDownRatio) < tolerance
                                                && !entity.IsDestroyed
                         )
                        .SetCount(1)
                        .SetTimeout(1f)
                        .Build();
        }

        public static IEnumerator IsNeedSlowDown(bool isNeedSlowDown)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                        .SetConditionName($"Is need slow down: {isNeedSlowDown}")
                        .SetEntityFilter(entity => entity.IsPlayer
                                                && entity.IsNeedSlowDown == isNeedSlowDown
                                                && !entity.IsDestroyed
                         )
                        .SetCount(1)
                        .SetTimeout(1f)
                        .Build();
        }

        public static IEnumerator IsControllableSlideInterrupted(bool isInterrupted, float timeout = 1f)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                        .SetConditionName($"Is controllable slide interrupted: {isInterrupted}")
                        .SetEntityFilter(entity => entity.IsPlayer && 
                                                   entity.IsControllableSlideInterrupted == isInterrupted &&
                                                   !entity.IsDestroyed)
                        .SetCount(1)
                        .SetTimeout(timeout)
                        .Build();
        }
        
        public static IEnumerator WaitForComboMultiplier(float expectedComboMultiplier, float timeout = 3f)
        {
            const float tolerance = 0.1f;
            var scoreService = DiUtils.Container.Resolve<IScoreService>();

            yield return AssertUtils.WaitFor(() => Math.Abs(scoreService.Combo.CurrentValue - expectedComboMultiplier) < tolerance, timeout);
        }
        
        public static IEnumerator ComboWordType(EComboWordType comboWordType, float timeout = 3f)
        {
            var comboView = Object.FindObjectOfType<ComboView>();
            var scoreParameters = DiUtils.Container.Resolve<IScoreParameters>();

            yield return AssertUtils.WaitFor(() =>
                {
                    var word = comboView.ComboText.text;
                    var currentType = scoreParameters.ColumnWords.Contains(word)
                        ? EComboWordType.Column
                        : EComboWordType.Default;
                    return currentType == comboWordType;
                },
                timeout
            );
        }

        public static IEnumerator IsPreTurnReactActive(bool isActive, float timeout = 1f)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                .SetConditionName($"Is pre react turn active: {isActive}")
                .SetEntityFilter(entity => entity.IsPlayer && 
                                           entity.IsPreTurnReact == isActive &&
                                           !entity.IsDestroyed)
                .SetCount(1)
                .SetTimeout(timeout)
                .Build();
        }
        
        public static IEnumerator IsTurning(bool isTurning, float timeout = 4f)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                        .SetConditionName($"Is turning: {isTurning}")
                        .SetEntityFilter(entity => entity.IsPlayer && 
                                                   entity.HasTurning == isTurning &&
                                                   !entity.IsDestroyed)
                        .SetCount(1)
                        .SetTimeout(timeout)
                        .Build();
        }
        
        public static IEnumerator ReadyToPassObstacle(EObstaclePassType obstaclePassType, float timeout = 10)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                .SetConditionName($"Player is ready to pass obstacle {obstaclePassType}")
                .SetEntityFilter(entity => entity.IsPlayer 
                                           && entity.HasReadyToObstaclePass 
                                           && entity.ReadyToObstaclePass.Value.PassType == obstaclePassType 
                                           && !entity.IsDestroyed)
                .SetCount(1)
                .SetTimeout(timeout)
                .Build();
        }

        public static IEnumerator ReadyToPassAnyObstacle(float timeout = 10)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                        .SetConditionName($"Player is ready to pass obstacle")
                        .SetEntityFilter(entity => entity.IsPlayer 
                                                && entity.HasReadyToObstaclePass 
                                                && !entity.IsDestroyed)
                        .SetCount(1)
                        .SetTimeout(timeout)
                        .Build();
        }
        
        public static IEnumerator IsInPassOfObstacle(float timeout = 10)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                .SetConditionName("Is is pass of obstacle")
                .SetEntityFilter(entity => entity.IsPlayer
                                           && entity.HasReadyToObstaclePass
                                           && entity.HasObstaclePassReact
                                           && entity.HasPassObstacleMovePoints
                                           && !entity.IsDestroyed
                )
                .SetCount(1)
                .SetTimeout(timeout)
                .Build();
        }
        
        public static IEnumerator NotReadyToPassObstacle(float timeout = 10)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                .SetConditionName($"Player is not ready to pass obstacle")
                .SetEntityFilter(entity => entity.IsPlayer 
                                           && !entity.HasReadyToObstaclePass
                                           && !entity.IsDestroyed)
                .SetCount(1)
                .SetTimeout(timeout)
                .Build();
        }
        
        public static IEnumerator IsComboMove(float timeout = 10)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                .SetConditionName("Is ready for combo action move")
                .SetEntityFilter(entity => entity.IsPlayer && entity.IsReadyForFastActionMove && !entity.IsDestroyed)
                .SetCount(1)
                .SetTimeout(timeout)
                .Build();
        }
        
        public static IEnumerator IsNotComboMove(float timeout = 10)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                .SetConditionName("Is not ready for combo action move")
                .SetEntityFilter(entity => entity.IsPlayer && !entity.IsReadyForFastActionMove && !entity.IsDestroyed)
                .SetCount(1)
                .SetTimeout(timeout)
                .Build();
        }
    }
}