using System.Collections;
using Db.Player;
using Db.Player.Impl;
using Game.Utils.Movement;
using Tests.Playable.Utils;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.TestTools;
using ZenjectUtil.Test.Extensions;

namespace Tests.Playable.Gameplay.PlayerMovement
{
    public class SpeedAccelerationTest : AIntegrationTest
    {
        private const string TEST_SCENE = "SpeedAcceleration_TestScene";
        
        protected override void SubstituteResources()
        {
            //ScenesBase.SetScene(TEST_SCENE);
            var playerAccelerationSettings = Resources.Load<PlayerAccelerationParametersBase>("PlayerAccelerationParametersBase_Test");
            ResourceSubstitute.AddSubstitute<IPlayerAccelerationParametersBase>(playerAccelerationSettings);
        }

        //[UnityTest]
        public IEnumerator AccelerationSuccess()
        {
            yield return GameplayUtils.Load();
            
            yield return PlayerAssert.IsGrounded();
            yield return PlayerAssert.IsReadyForFastActionMove();
            
            yield return GameplayUtils.MakeSlide();
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.LongSlide);
            yield return PlayerAssert.IsInFastActionMove();
            
            yield return PlayerAssert.IsNotReadyForFastActionMove();
            yield return PlayerAssert.IsSpeedAccelerate();
            yield return PlayerAssert.IsNotDead();
            yield return PlayerAssert.IsNotDamaged(1);

            yield return PlayerAssert.IsSpeedNotAccelerate();

            yield return new WaitForSeconds(2f);
        }
        
        //[UnityTest]
        public IEnumerator AccelerationFail()
        {
            yield return GameplayUtils.Load();
            
            yield return PlayerAssert.IsGrounded();
            yield return PlayerAssert.IsNotReadyForFastActionMove();
            
            yield return GameplayUtils.MakeSlide();
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.LongSlide);
            yield return PlayerAssert.IsNotInFastActionMove();
            
            yield return PlayerAssert.IsNotDead();
            yield return PlayerAssert.IsNotDamaged(1);
            
            yield return new WaitForSeconds(3f);
            
            yield return PlayerAssert.IsSpeedNotAccelerate();
        }
        
        //[UnityTest]
        public IEnumerator SlideAndJumpAfterAcceleration()
        {
            yield return GameplayUtils.Load();
            
            yield return PlayerAssert.IsGrounded();
            yield return PlayerAssert.IsReadyForFastActionMove();
            
            yield return GameplayUtils.MakeSlide();
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.LongSlide);
            yield return PlayerAssert.IsInFastActionMove();

            yield return new WaitForSeconds(2f);

            var game = Contexts.SharedInstance.Game;
            var player = game.PlayerEntity;
            var oldSpeed = player.ForwardSpeed.Value.Current;
            
            yield return GameplayUtils.MakeJump();
            
            yield return new WaitForSeconds(0.1f);
            
            var currentSpeed = player.ForwardSpeed.Value.Current;
            
            var isNotEqual = !Mathf.Approximately(oldSpeed, currentSpeed);
            
            Assert.IsTrue(isNotEqual);
            
            yield return new WaitForSeconds(1f);
            
            oldSpeed = player.ForwardSpeed.Value.Current;
            
            yield return GameplayUtils.MakeSlide();
            
            yield return new WaitForSeconds(0.1f);
            
            currentSpeed = player.ForwardSpeed.Value.Current;
            
            isNotEqual = !Mathf.Approximately(oldSpeed, currentSpeed);
            
            Assert.IsTrue(isNotEqual);
            
            yield return new WaitForSeconds(3f);
        }
    }
}