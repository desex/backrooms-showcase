using System.Collections;
using Game.Utils.Movement;
using NUnit.Framework;
using Tests.Playable.Utils;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Playable.Gameplay.PlayerMovement
{
    public class DoubleJumpOverWideHoleTest : AIntegrationTest
    {
        private const string TEST_SCENE = "DoubleJumpOverWideHole_TestScene";

        protected override void SubstituteResources()
        {
            //ScenesBase.SetScene(TEST_SCENE);
        }
        
        //[UnityTest]
        [Category(TestCategoriesNames.MOVEMENT)]
        public IEnumerator CanJumpOver_WideHole_WithDoubleJump()
        {
            yield return GameplayUtils.Load();

            yield return new WaitForSeconds(3.3f);

            yield return DoubleJumpWithDelay(0.4f);
            
            yield return new WaitForSeconds(3.6f);
            
            yield return DoubleJumpWithDelay(0.4f);
            
            yield return new WaitForSeconds(2f);
        }
        
        //[UnityTest]
        [Category(TestCategoriesNames.MOVEMENT)]
        public IEnumerator CanJumpOut_FromWideHole_WithSwipe()
        {
            yield return GameplayUtils.Load();

            yield return new WaitForSeconds(3.7f);

            yield return GameplayUtils.Swipe(Vector2.up * 80);
            
            yield return new WaitForSeconds(4.25f);
            
            yield return GameplayUtils.Swipe(Vector2.up * 80);
            
            yield return new WaitForSeconds(2f);
        }
        
        //[UnityTest]
        [Category(TestCategoriesNames.MOVEMENT)]
        public IEnumerator CanJumpOut_FromWideHole_WithSecondSwipe()
        {
            yield return GameplayUtils.Load();
            
            yield return new WaitForSeconds(3.3f);

            yield return DoubleJumpWithDelay(0.6f);
            
            yield return new WaitForSeconds(3f);
            
            yield return DoubleJumpWithDelay(0.6f);
            
            yield return new WaitForSeconds(2f);
        }
        
        //[UnityTest]
        [Category(TestCategoriesNames.MOVEMENT)]
        public IEnumerator CanClimbOut_FromWideHole_WithSwipe()
        {
            yield return GameplayUtils.Load();

            yield return PlayerAssert.IsReadyToComeback();

            yield return GameplayUtils.Swipe(Vector2.up * 80);
            
            yield return new WaitForSeconds(2f);
        }
        
        private static IEnumerator DoubleJumpWithDelay(float delay)
        {
            yield return GameplayUtils.Swipe(Vector2.up * 80);

            yield return PlayerAssert.Jumping(EJumpType.Jump);
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Jump, 1f);

            yield return new WaitForSeconds(delay);

            yield return GameplayUtils.Swipe(Vector2.up * 80);

            yield return PlayerAssert.Jumping(EJumpType.DoubleJump);
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Jump, 1f);

            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Run, 2f);
            yield return PlayerAssert.NotJumping();
        }
    }
}