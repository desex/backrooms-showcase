using System.Collections;
using Game.Utils.Movement;
using Tests.Playable.Utils;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Playable.Gameplay.PlayerMovement.Slides
{
    public class SlideTest : AIntegrationTest
    {
        private const string SLIDE_TEST_SCENE = "Slide_TestScene";
        private const int SUCCESSFUL_SLIDES_COUNT = 2;

        protected override void SubstituteResources()
        {
            //ScenesBase.SetScene(SLIDE_TEST_SCENE);
        }

        //[UnityTest]
        public IEnumerator SuccessSlide()
        {
            yield return GameplayUtils.Load();

            yield return new WaitForSeconds(1f);

            for (var i = 0; i < SUCCESSFUL_SLIDES_COUNT; i++)
            {
                yield return GameplayUtils.MakeSlide();
                yield return PlayerAssert.BodyMoveType(EBodyMoveType.ShortSlide);
                yield return new WaitForSeconds(2f);
            }
        }
        
        //[UnityTest]
        public IEnumerator NotSuccessSlide()
        {
            yield return GameplayUtils.Load();

            yield return new WaitForSeconds(0.5f);
            
            yield return PlayerAssert.NotBodyMoveType(EBodyMoveType.ShortSlide);

            yield return new WaitForSeconds(1.5f);
        }
        
        //[UnityTest]
        public IEnumerator CancelSlide()
        {
            yield return GameplayUtils.Load();

            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Run);
            
            yield return new WaitForSeconds(2f);

            yield return GameplayUtils.MakeSlide();
            
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.ShortSlide);
            
            yield return new WaitForSeconds(0.2f);
            
            yield return GameplayUtils.SmoothSwipe(Vector2.up);

            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Run, 0.1f);
            
            yield return new WaitForSeconds(2f);
        }
    }
}