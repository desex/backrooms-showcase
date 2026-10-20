using System.Collections;
using NUnit.Framework;
using Tests.Playable.Utils;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Playable.Gameplay.Obstacle
{
    public class MonkeyJumpObstaclesTest : AIntegrationTest
    {
        private const string FALL_SCIENTIST_TEST_SCENE = "MonkeyJumpObstacles_TestScene";

        protected override void SubstituteResources()
        {
            //ScenesBase.SetScene(FALL_SCIENTIST_TEST_SCENE);
        }
        
        //[UnityTest]
        [Category(TestCategoriesNames.OBSTACLES)]
        public IEnumerator DoubleJumpForMonkeyJumpObstacles()
        {
            yield return GameplayUtils.Load();
            
            yield return new WaitForSeconds(4.8f);
            yield return GameplayUtils.MakeDoubleJump();
            
            yield return new WaitForSeconds(1.1f);
            yield return GameplayUtils.MakeDoubleJump();
            
            yield return new WaitForSeconds(0.8f);
            yield return GameplayUtils.MakeDoubleJump();
            
            yield return new WaitForSeconds(1.05f);
            yield return GameplayUtils.MakeDoubleJump();   
            
            yield return new WaitForSeconds(0.8f);
            yield return GameplayUtils.MakeDoubleJump();
            
            yield return new WaitForSeconds(2f);
        }
    }
}