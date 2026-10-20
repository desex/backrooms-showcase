using System.Collections;
using Tests.Playable.Utils;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Playable.Gameplay.PlayerMovement
{
    public class UpSurfaceTest : AIntegrationTest
    {
        private const string TEST_SCENE = "RunUpSurface_TestScene";
        
        protected override void SubstituteResources()
        {
            //ScenesBase.SetScene(TEST_SCENE);
        }
        
        //[UnityTest]
        public IEnumerator RunUpSurface()
        {
            yield return GameplayUtils.Load();

            yield return PlayerAssert.IsNotRunningUpSurface();
            
            yield return new WaitForSeconds(1.5f);
            
            yield return PlayerAssert.IsRunningUpSurface();
            
            yield return new WaitForSeconds(5f);
            
            yield return PlayerAssert.IsNotRunningUpSurface();

            yield return new WaitForSeconds(1f);
        }
    }
}