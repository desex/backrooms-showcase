using System.Collections;
using Db.Score;
using Game.Services.Score;
using Game.Utils.Movement;
using NUnit.Framework;
using Tests.Helpers.Mock;
using Tests.Playable.UITest.Utils;
using Tests.Playable.Utils;
using UnityEngine;
using UnityEngine.TestTools;
using ZenjectUtil.Test.Extensions;
using Assert = ModestTree.Assert;

namespace Tests.Playable.Gameplay.PlayerMovement
{
    public class MonkeyJumpTest : AIntegrationTest
    {
        private const string MONKEY_JUMP_TEST_SCENE = "MonkeyJump_TestScene";
        private const int SUCCESSFUL_JUMPS_COUNT = 2;

        protected override void SubstituteResources()
        {
            //ScenesBase.SetScene(MONKEY_JUMP_TEST_SCENE);
            
            var scoreParameters = new Mock.ScoreParameters();
            ResourceSubstitute.AddSubstitute<IScoreParameters>(scoreParameters);
        }
        
        [Category(TestCategoriesNames.MOVEMENT)]
        public IEnumerator FastMonkeyJumpSuccess()
        {
            Assert.IsEqual(true, false); //TODO: delete this when will be repair tests and fix test or just DELETE IT
            yield return GameplayUtils.Load();

            GameplayUtils.SetPlayerPosition(Vector3.zero);
            
            for (var i = 0; i < 2; i++)
            {
                PlayerAssert.Score(i * 2);
                
                yield return PlayerAssert.IsGrounded();
                yield return PlayerAssert.ReadyToPassObstacle(EObstaclePassType.MonkeyJump);
                yield return PlayerAssert.IsComboMove();
                
                yield return GameplayUtils.Swipe(Vector2.up * 80);
                yield return PlayerAssert.IsInPassOfObstacle();
                yield return PlayerAssert.NotReadyToPassObstacle();
                
                yield return PlayerAssert.IsNotDead();
                yield return PlayerAssert.IsNotDamaged(2);
                
                PlayerAssert.ComboMultiplier(i + 1);
            }
            
            PlayerAssert.Score(6);

            yield return new WaitForSeconds(2f);
        }
        
        [Category(TestCategoriesNames.MOVEMENT)]
        public IEnumerator NormalMonkeyJumpSuccess()
        {
            Assert.IsEqual(true, false); //TODO: delete this when will be repair tests and fix test or just DELETE IT
            yield return GameplayUtils.Load();

            GameplayUtils.SetPlayerPosition(Vector3.zero);
            
            for (var i = 0; i < SUCCESSFUL_JUMPS_COUNT; i++)
            {
                yield return PlayerAssert.IsGrounded();
                yield return PlayerAssert.ReadyToPassObstacle(EObstaclePassType.MonkeyJump);
                yield return PlayerAssert.IsNotComboMove();
                
                yield return GameplayUtils.MakeJump();
                yield return PlayerAssert.IsInPassOfObstacle();
                yield return PlayerAssert.IsNotComboMove();
                
                yield return PlayerAssert.NotReadyToPassObstacle();
                yield return PlayerAssert.IsNotDead();
                yield return PlayerAssert.IsNotDamaged(2);
                
                PlayerAssert.Score(i + 1);
            }

            yield return new WaitForSeconds(2f);
        }
        
        [Category(TestCategoriesNames.MOVEMENT)]
        public IEnumerator MonkeyJumpFail()
        {
            Assert.IsEqual(true, false); //TODO: delete this when will be repair tests and fix test or just DELETE IT
            yield return GameplayUtils.Load();
            
            GameplayUtils.SetPlayerPosition(Vector3.zero);
            
            yield return PlayerAssert.IsGrounded();
            yield return PlayerAssert.ReadyToPassObstacle(EObstaclePassType.MonkeyJump);
            yield return PlayerAssert.IsDamaged();
            
            yield return new WaitForSeconds(1f);
        }
        
        [Category(TestCategoriesNames.MOVEMENT)]
        public IEnumerator SwipeAfterComboMoveTrigger()
        {
            Assert.IsEqual(true, false); //TODO: delete this when will be repair tests and fix test or just DELETE IT
            yield return GameplayUtils.Load();

            GameplayUtils.SetPlayerPosition(Vector3.zero);
            
            yield return PlayerAssert.IsGrounded();
            yield return PlayerAssert.ReadyToPassObstacle(EObstaclePassType.MonkeyJump);
            yield return PlayerAssert.IsComboMove();
            yield return PlayerAssert.IsNotComboMove();
            yield return GameplayUtils.MakeJump();
            
            yield return PlayerAssert.IsDamaged();

            yield return new WaitForSeconds(2f);
        }
        
        [Category(TestCategoriesNames.MOVEMENT)]
        //[Category(TestCategoriesNames.VISUAL)]
        public IEnumerator SlideInMonkeyJump()
        {
            Assert.IsEqual(true, false); //TODO: delete this when will be repair tests and fix test or just DELETE IT
            yield return GameplayUtils.Load();
            
            GameplayUtils.SetPlayerPosition(Vector3.zero);
            
            yield return PlayerAssert.IsGrounded();
            yield return PlayerAssert.ReadyToPassObstacle(EObstaclePassType.MonkeyJump);
            yield return GameplayUtils.Swipe(Vector2.down);

            yield return PlayerAssert.IsDamaged();
            
            yield return new WaitForSeconds(1f);
        }
        
        [Category(TestCategoriesNames.MOVEMENT)]
        //[Category(TestCategoriesNames.VISUAL)]
        public IEnumerator SlideAndMonkeyJump()
        {
            Assert.IsEqual(true, false); //TODO: delete this when will be repair tests and fix test or just DELETE IT
            yield return GameplayUtils.Load();
            
            GameplayUtils.SetPlayerPosition(Vector3.zero);
            
            yield return PlayerAssert.IsGrounded();
            yield return PlayerAssert.ReadyToPassObstacle(EObstaclePassType.MonkeyJump);
            yield return GameplayUtils.Swipe(Vector2.down);
            
            yield return new WaitForSeconds(0.3f);
            
            yield return GameplayUtils.Swipe(Vector2.up);
            yield return GameplayUtils.Swipe(Vector2.up);
            
            yield return new WaitForSeconds(1f);
        }
    }
}