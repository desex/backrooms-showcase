using System.Collections;
using Tests.Playable.Utils;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Playable.Gameplay.PlayerMovement
{
    public class PlayerRunTest : AIntegrationTest
    {
        private const string TEST_SCENE = "PlayerRunTest_TestScene";
        private const string TRUCK_OBSTACLE_TEST_SCENE = "TruckObstacle_TestScene";
        
        protected override void SubstituteResources()
        {
            //ScenesBase.SetScene(TEST_SCENE);
        }

        //[UnityTest]
        public IEnumerator PlayerRun()
        {
            yield return GameplayUtils.LoadWithDelay(3f);
            
            yield return PlayerAssert.PlayerRun();
            yield return new WaitForSeconds(5f);
        }
        
        //[UnityTest]
        public IEnumerator MetalRun()
        {
            //ScenesBase.SetScene(TRUCK_OBSTACLE_TEST_SCENE);
            
            yield return GameplayUtils.Load();

            yield return GameplayUtils.SmoothSwipe(Vector2.right);
            
            yield return PlayerAssert.PlayerRun();
            yield return new WaitForSeconds(5f);
        }
    }
}