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
    public class ForwardJumpTest : AIntegrationTest
    {
        private const string FORWARD_JUMP_TEST_SCENE = "ForwardJump_TestScene";
        
        protected override void SubstituteResources()
        {
            //ScenesBase.SetScene(FORWARD_JUMP_TEST_SCENE);
            
            var scoreParameters = new Mock.ScoreParameters();
            ResourceSubstitute.AddSubstitute<IScoreParameters>(scoreParameters);
        }
        
        //[UnityTest]
        [Category(TestCategoriesNames.MOVEMENT)]
        [Category(TestCategoriesNames.SOUND)]
        public IEnumerator FastForwardJumpSuccess()
        {
            yield return GameplayUtils.Load();

            for (var i = 0; i < 2; i++)
            {
                PlayerAssert.Score(i * 2);
                yield return PlayerAssert.IsGrounded();
                yield return PlayerAssert.IsReadyForFastActionMove();
                yield return GameplayUtils.Swipe(Vector2.up * 80);
                yield return PlayerAssert.BodyMoveType(EBodyMoveType.ForwardJump);
                yield return PlayerAssert.IsInFastActionMove(2);
                yield return new WaitForSeconds(0.3f);
                yield return PlayerAssert.IsNotDead();
                yield return PlayerAssert.IsNotDamaged(2);
            }
            
            PlayerAssert.ComboMultiplier(2);
            PlayerAssert.Score(6);

            yield return new WaitForSeconds(2f);
        }
        
        //[UnityTest]
        public IEnumerator NormalForwardJumpSuccess()
        {
            yield return GameplayUtils.Load();

            for (var i = 0; i < 2; i++)
            {
                yield return PlayerAssert.IsGrounded();
                yield return PlayerAssert.IsNotReadyForFastActionMove();
                yield return GameplayUtils.Swipe(Vector2.up * 80);
                yield return PlayerAssert.BodyMoveType(EBodyMoveType.ForwardJump);
                yield return PlayerAssert.IsNotInFastActionMove();
                yield return new WaitForSeconds(0.3f);
                yield return PlayerAssert.IsNotDead();
                yield return PlayerAssert.IsNotDamaged(2);
                
                PlayerAssert.Score(i + 1);
                PlayerAssert.ComboMultiplier(1);
            }

            yield return new WaitForSeconds(2f);
        }

        //[UnityTest]
        public IEnumerator ForwardJumpFail()
        {
            yield return GameplayUtils.Load();
            
            yield return PlayerAssert.IsGrounded();
            yield return PlayerAssert.IsDamaged();

            yield return new WaitForSeconds(1f);
        }
        
        //[UnityTest]
        [Category(TestCategoriesNames.MOVEMENT)]
        [Category(TestCategoriesNames.SOUND)]
        public IEnumerator PlayerNotStack_InFrontOfForwardJumpObstacle_AfterSmallSwipeUp()
        {
            yield return GameplayUtils.Load();
            
            yield return GameplayUtils.Swipe(Vector2.left * 80);
            
            yield return new WaitForSeconds(3f);
            
            yield return GameplayUtils.Swipe(Vector2.up * 5);
            
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.ForwardJump);

            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Run);

            yield return new WaitForSeconds(2f);
        }
    }
}