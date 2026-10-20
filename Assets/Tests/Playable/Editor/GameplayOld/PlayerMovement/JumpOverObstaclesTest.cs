using System.Collections;
using Db.Score;
using Game.Utils.Movement;
using ModestTree;
using Tests.Helpers.Mock;
using Tests.Playable.Utils;
using UnityEngine;
using UnityEngine.TestTools;
using ZenjectUtil.Test.Extensions;

namespace Tests.Playable.Gameplay.PlayerMovement
{
    public class JumpOverObstaclesTest : AIntegrationTest
    {
        private const string JUMP_OVER_OBSTACLE_TEST_SCENE = "JumpOverObstacle_TestScene";
        private const string JUMP_OVER_OBSTACLE_ON_DIAGONAL_TEST_SCENE = "JumpOverObstacleOnDiagonal_TestScene";
        
        protected override void SubstituteResources()
        {
            //ScenesBase.SetScene(JUMP_OVER_OBSTACLE_TEST_SCENE);
            
            var scoreParameters = new Mock.ScoreParameters();
            ResourceSubstitute.AddSubstitute<IScoreParameters>(scoreParameters);
        }
        
        //[UnityTest]
        public IEnumerator FastJumpOverObstacleSuccess()
        {
            yield return GameplayUtils.Load();

            for (var i = 0; i < 2; i++)
            {
                PlayerAssert.Score(i * 2);
                
                yield return PlayerAssert.IsGrounded();
                yield return PlayerAssert.IsReadyToJumpOverObstacle();
                yield return PlayerAssert.IsReadyForFastActionMove();
                yield return GameplayUtils.Swipe(Vector2.up * 80);
                yield return PlayerAssert.BodyMoveType(EBodyMoveType.Jump);
                yield return new WaitForSeconds(0.3f);
                yield return PlayerAssert.IsNotReadyToJumpOverObstacle();
                yield return PlayerAssert.IsNotDead();
                yield return PlayerAssert.IsNotDamaged(2);
            }
            
            PlayerAssert.ComboMultiplier(2);
            PlayerAssert.Score(6);

            yield return new WaitForSeconds(2f);
        }
        
        //[UnityTest]
        public IEnumerator NormalJumpOverObstacleSuccess()
        {
            yield return GameplayUtils.Load();

            for (var i = 0; i < 2; i++)
            {
                yield return PlayerAssert.IsGrounded();
                yield return PlayerAssert.IsReadyToJumpOverObstacle();
                yield return PlayerAssert.IsNotReadyForFastActionMove();
                yield return GameplayUtils.Swipe(Vector2.up * 80);
                yield return PlayerAssert.BodyMoveType(EBodyMoveType.Jump);
                yield return PlayerAssert.IsNotReadyToJumpOverObstacle();
                yield return PlayerAssert.IsNotDead();
                yield return PlayerAssert.IsNotDamaged(2);
                
                PlayerAssert.Score(i + 1);
                PlayerAssert.ComboMultiplier(1);
            }

            yield return new WaitForSeconds(2f);
        }
        
        //[UnityTest]
        public IEnumerator JumpOverObstacleFail()
        {
            yield return GameplayUtils.Load();
            
            yield return PlayerAssert.IsGrounded();
            yield return PlayerAssert.IsReadyToJumpOverObstacle();
            yield return PlayerAssert.IsDamaged();
            
            yield return new WaitForSeconds(1f);
        }

        //[UnityTest]
        public IEnumerator MonkeyJumpOnDiagonal()
        {
            Assert.IsEqual(true, false); //TODO: delete this when will be repair tests
            //ScenesBase.SetScene(JUMP_OVER_OBSTACLE_ON_DIAGONAL_TEST_SCENE);
            
            yield return GameplayUtils.Load();

            //yield return PlayerAssert.IsReadyToMonkeyJump(); //TODO: fix it when will be repair tests

            yield return GameplayUtils.MakeJump();

            yield return new WaitForSeconds(4f);
        }
    }
}