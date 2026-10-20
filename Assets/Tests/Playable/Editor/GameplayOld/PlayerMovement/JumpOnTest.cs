using System.Collections;
using Db.Score;
using Game.Utils.Movement;
using NUnit.Framework;
using Tests.Helpers.Mock;
using Tests.Playable.Utils;
using UnityEngine;
using UnityEngine.TestTools;
using ZenjectUtil.Test.Extensions;

namespace Tests.Playable.Gameplay.PlayerMovement
{
    public class JumpOnTest : AIntegrationTest
    {
        private const string TEST_SCENE = "JumpOn_TestScene";
        
        protected override void SubstituteResources()
        {
            //ScenesBase.SetScene(TEST_SCENE);
            
            var scoreParameters = new Mock.ScoreParameters();
            ResourceSubstitute.AddSubstitute<IScoreParameters>(scoreParameters);
        }
        
        //[UnityTest]
        [Category(TestCategoriesNames.MOVEMENT)]
        public IEnumerator JumpOnSuccess()
        {
            yield return GameplayUtils.Load();

            GameplayUtils.SetPlayerPosition(Vector3.zero);
            
            for (var i = 0; i < 2; i++)
            {
                yield return PlayerAssert.IsGrounded();
                yield return PlayerAssert.IsReadyToJumpOn();
                yield return PlayerAssert.IsNotReadyForFastActionMove();
                yield return GameplayUtils.Swipe(Vector2.up * 80);
                yield return PlayerAssert.BodyMoveType(EBodyMoveType.JumpOn);
                yield return PlayerAssert.IsNotInFastActionMove();
                yield return PlayerAssert.IsNotReadyToJumpOn();
                yield return PlayerAssert.IsNotDead();
                yield return PlayerAssert.IsNotDamaged(2);
                
                PlayerAssert.Score(i + 1);
                PlayerAssert.ComboMultiplier(1);
            }

            yield return new WaitForSeconds(2f);
        }
        
        //[UnityTest]
        [Category(TestCategoriesNames.MOVEMENT)]
        public IEnumerator JumpOnFail()
        {
            yield return GameplayUtils.Load();

            GameplayUtils.SetPlayerPosition(Vector3.zero);
            
            yield return PlayerAssert.IsGrounded();
            yield return PlayerAssert.IsReadyToJumpOn();
            yield return PlayerAssert.IsDamaged();
            
            yield return new WaitForSeconds(1f);
        }
        
        //[UnityTest]
        [Category(TestCategoriesNames.MOVEMENT)]
        public IEnumerator FastJumpOnSuccess()
        {
            yield return GameplayUtils.Load();

            GameplayUtils.SetPlayerPosition(Vector3.zero);
            
            for (var i = 0; i < 2; i++)
            {
                PlayerAssert.Score(i * 2);
                
                yield return PlayerAssert.IsGrounded();
                yield return PlayerAssert.IsReadyToJumpOn();
                yield return PlayerAssert.IsReadyForFastActionMove();
                yield return GameplayUtils.Swipe(Vector2.up * 80);
                yield return PlayerAssert.BodyMoveType(EBodyMoveType.JumpOn);
                yield return PlayerAssert.IsInFastActionMove();
                yield return PlayerAssert.IsNotReadyToJumpOn();
                yield return PlayerAssert.IsNotReadyForFastActionMove();
                yield return PlayerAssert.IsNotDead();
                yield return PlayerAssert.IsNotDamaged(2);
            }
            
            PlayerAssert.ComboMultiplier(2);
            PlayerAssert.Score(6);

            yield return new WaitForSeconds(2f);
        }
    }
}