#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
using System.Collections;
using Game.UI.GameRestartButton;
using Game.Utils.Movement;
using Tests.Playable.Utils;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Playable.Gameplay.PlayerMovement.Slides
{
    public class DoubleSlideTest : AIntegrationTest
    {
        private const string SLIDE_TEST_SCENE = "Slide_TestScene";

        protected override void SubstituteResources()
        {
            //ScenesBase.SetScene(SLIDE_TEST_SCENE);
        }
        
        //[UnityTest]
        public IEnumerator DoubleSlideSuccess()
        {
            yield return GameplayUtils.Load();

            yield return new WaitForSeconds(0.5f);
            
            yield return GameplayUtils.MakeSlide();
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.ShortSlide);
                
            yield return new WaitForSeconds(0.3f);
                
            yield return GameplayUtils.MakeSlide();
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.ShortSlide);
                
            yield return GameplayUtils.MakeSlide();
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.ShortSlide);
                
            yield return GameplayUtils.MakeSlide();
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.ShortSlide);
            
            yield return new WaitForSeconds(0.6f);
            
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Run);

            yield return new WaitForSeconds(1.5f);
        }
        
        //[UnityTest]
        public IEnumerator TripleSlideSuccess()
        {
            yield return GameplayUtils.Load();

            yield return new WaitForSeconds(0.5f);
            
            var changeSlidesButton = Object.FindObjectOfType<DebugView>();
            changeSlidesButton.ChangeSlidesButton.onClick?.Invoke();
            
            yield return new WaitForSeconds(1f);
            
            yield return GameplayUtils.MakeSlide();
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.ShortSlide);
                
            yield return new WaitForSeconds(0.3f);
                
            yield return GameplayUtils.MakeSlide();
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.ShortSlide);
                
            yield return new WaitForSeconds(0.2f);
                
            yield return GameplayUtils.MakeSlide();
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.ShortSlide);
                
            yield return GameplayUtils.MakeSlide();
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.ShortSlide);
            
            yield return GameplayUtils.MakeSlide();
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.ShortSlide);
            
            yield return new WaitForSeconds(1f);
            
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Run);
            
            yield return new WaitForSeconds(1.5f);
        }
        
        //[UnityTest]
        public IEnumerator SlideDoubleSlideTripleSlideSuccess()
        {
            yield return GameplayUtils.Load();

            yield return new WaitForSeconds(0.5f);
            
            yield return GameplayUtils.MakeSlide();
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.ShortSlide);
                
            yield return new WaitForSeconds(2f);
            
            yield return GameplayUtils.MakeSlide();
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.ShortSlide);
                
            yield return new WaitForSeconds(0.3f);
                
            yield return GameplayUtils.MakeSlide();
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.ShortSlide);
                
            yield return new WaitForSeconds(1f);
            
            var changeSlidesButton = Object.FindObjectOfType<DebugView>();
            changeSlidesButton.ChangeSlidesButton.onClick?.Invoke();
            
            yield return new WaitForSeconds(1f);
            
            yield return GameplayUtils.MakeSlide();
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.ShortSlide);
                
            yield return new WaitForSeconds(0.3f);
                
            yield return GameplayUtils.MakeSlide();
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.ShortSlide);
                
            yield return new WaitForSeconds(0.2f);
                
            yield return GameplayUtils.MakeSlide();
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.ShortSlide);
                
            yield return new WaitForSeconds(2f);

            yield return new WaitForSeconds(1.5f);
        }
    }
}
#endif