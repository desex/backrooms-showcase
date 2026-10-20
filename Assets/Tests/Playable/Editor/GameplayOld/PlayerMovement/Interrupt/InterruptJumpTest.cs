using System.Collections;
using Tests.Playable.Utils;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Playable.Gameplay.PlayerMovement.Interrupt
{
    public class InterruptJumpTest : AIntegrationTest
    {
        private const string TEST_SCENE = "InterruptJump_TestScene";
        
        protected override void SubstituteResources()
        {
            //ScenesBase.SetScene(TEST_SCENE);
        }

        //[UnityTest]
        public IEnumerator InterruptJump()
        {
            yield return GameplayUtils.Load();
            
            yield return PlayerAssert.IsGrounded();

            yield return new WaitForSeconds(1f);

            yield return GameplayUtils.Swipe(Vector2.up * 80);
            yield return PlayerAssert.IsNotGrounded();

            yield return new WaitForSeconds(.3f);
            
            yield return GameplayUtils.Swipe(Vector2.down * 80);
            yield return PlayerAssert.IsInterrupted();
            
            yield return PlayerAssert.IsGrounded();

            yield return new WaitForSeconds(2f);
        }
    }
}