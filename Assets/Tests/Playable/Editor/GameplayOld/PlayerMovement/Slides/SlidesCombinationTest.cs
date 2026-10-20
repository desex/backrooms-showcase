using System.Collections;
using Game.Utils.Movement;
using Tests.Playable.Utils;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Playable.Gameplay.PlayerMovement.Slides
{
    public class SlidesCombinationTest : AIntegrationTest
    {
        private const string TEST_SCENE = "SlidesCombination_TestScene";

        protected override void SubstituteResources()
        {
            //ScenesBase.SetScene(TEST_SCENE);
        }

        //[UnityTest]
        public IEnumerator LongAndControllableSlidesCombination()
        {
            yield return GameplayUtils.Load();

            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Run);
            
            yield return new WaitForSeconds(2.5f);

            yield return GameplayUtils.MakeSlide();
            
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.LongSlide);
            
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.ControllableSlide);
            
            yield return new WaitForSeconds(3f);
            
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Run);
            
            yield return new WaitForSeconds(0.5f);
        }
    }
}