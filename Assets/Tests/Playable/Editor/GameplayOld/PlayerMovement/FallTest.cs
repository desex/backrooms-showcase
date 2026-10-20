using System.Collections;
using Db.Score;
using Game.Utils.Movement;
using Tests.Helpers.Mock;
using Tests.Playable.Utils;
using UnityEngine;
using UnityEngine.TestTools;
using ZenjectUtil.Test.Extensions;

namespace Tests.Playable.Gameplay.PlayerMovement
{
    public class FallTest : AIntegrationTest
    {
        private const string TEST_SCENE = "Fall_TestScene";
        
        protected override void SubstituteResources()
        {
            //ScenesBase.SetScene(TEST_SCENE);
            
            var scoreParameters = new Mock.ScoreParameters();
            ResourceSubstitute.AddSubstitute<IScoreParameters>(scoreParameters);
        }
        
        //[UnityTest]
        public IEnumerator RollDuringFall()
        {
            yield return GameplayUtils.Load();
            
            yield return new WaitForSeconds(3f);
            
            PlayerAssert.Score(3);
            PlayerAssert.ComboMultiplier(2);

            yield return PlayerAssert.IsGrounded();
            
            yield return PlayerAssert.IsReadyToFailFall();
            yield return PlayerAssert.IsNotGrounded();
            
            yield return GameplayUtils.Swipe(Vector2.down * 80);
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.InterruptFall);
            yield return PlayerAssert.IsNotReadyToFailFall();
            
            yield return PlayerAssert.IsGrounded();
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Roll);
            
            yield return PlayerAssert.IsNotDead();
            yield return PlayerAssert.IsNotDamaged(2);
            
            PlayerAssert.Score(6);
            PlayerAssert.ComboMultiplier(3);

            yield return new WaitForSeconds(2f);
        }
        
        //[UnityTest]
        public IEnumerator FailFall()
        {
            yield return GameplayUtils.Load();
            
            yield return new WaitForSeconds(3f);
            
            PlayerAssert.Score(3);
            PlayerAssert.ComboMultiplier(2);

            yield return PlayerAssert.IsGrounded();
            
            yield return PlayerAssert.IsReadyToFailFall();
            yield return PlayerAssert.IsNotGrounded();
            
            yield return PlayerAssert.IsGrounded();

            PlayerAssert.Score(3);
            PlayerAssert.ComboMultiplier(1);

            yield return new WaitForSeconds(2f);
        }
        
        //[UnityTest]
        public IEnumerator RollAfterJump()
        {
            yield return GameplayUtils.Load();

            yield return PlayerAssert.IsGrounded();
            yield return GameplayUtils.Swipe(Vector2.up * 80);
            
            yield return PlayerAssert.IsNotGrounded();
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Jump);
            
            yield return new WaitForSeconds(0.3f);
            
            yield return GameplayUtils.Swipe(Vector2.down * 80);
            yield return PlayerAssert.IsGrounded();
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Roll);
            
            yield return PlayerAssert.IsNotDead();
            yield return PlayerAssert.IsNotDamaged(2);

            yield return new WaitForSeconds(2f);
        }
    }
}