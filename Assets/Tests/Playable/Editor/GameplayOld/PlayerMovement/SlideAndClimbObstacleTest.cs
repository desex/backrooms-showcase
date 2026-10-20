using System.Collections;
using Game.Utils.Movement;
using Tests.Playable.Utils;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Playable.Gameplay.PlayerMovement
{
    public class SlideAndClimbObstacleTest : AIntegrationTest
    {
        private const string TEST_SCENE = "SlideAndClimbObstacles_TestScene";
        
        protected override void SubstituteResources()
        {
            //ScenesBase.SetScene(TEST_SCENE);
        }

        //[UnityTest]
        public IEnumerator SlideAndClimbObstacle()
        {
            yield return GameplayUtils.Load();

            yield return new WaitForSeconds(1f);
            
            yield return PlayerAssert.IsGrounded();
            yield return PlayerAssert.IsReadyToClimb();
            
            yield return GameplayUtils.MakeSlide();
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.LongSlide);
            
            yield return new WaitForSeconds(1f);
            
            yield return PlayerAssert.IsGrounded();
            yield return PlayerAssert.IsReadyToClimb();
            
            yield return GameplayUtils.MakeJump();
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Climb);
            yield return PlayerAssert.IsNotReadyToClimb();
            
            yield return new WaitForSeconds(2f);
        }
    }
}