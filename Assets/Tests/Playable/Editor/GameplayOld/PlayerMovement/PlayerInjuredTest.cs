using System.Collections;
using Tests.Playable.Utils;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Playable.Gameplay.PlayerMovement
{
    public class PlayerInjuredTest : AIntegrationTest
    {
        private const string TEST_SCENE = "PlayerInjured_TestScene";
        
        protected override void SubstituteResources()
        {
            //ScenesBase.SetScene(TEST_SCENE);
        }

        //[UnityTest]
        public IEnumerator PlayerRunInjured()
        {
            yield return GameplayUtils.Load();
            
            yield return new WaitForSeconds(0.5f);
            
            yield return PlayerAssert.PlayerRun();

            yield return PlayerAssert.IsInjured();
            
            yield return GameplayUtils.SmoothSwipe(Vector2.right);

            yield return new WaitForSeconds(2f);
        }
    }
}