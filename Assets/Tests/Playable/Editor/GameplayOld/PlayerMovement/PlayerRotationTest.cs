using System.Collections;
using Ecs.Views.Linkable.Impl.Player;
using NUnit.Framework;
using Tests.Playable.Utils;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Playable.Gameplay.PlayerMovement
{
    public class PlayerRotationTest : AIntegrationTest
    {
        private const string TEST_SCENE = "Rotation_TestScene";
        
        protected override void SubstituteResources()
        {
            //ScenesBase.SetScene(TEST_SCENE);
        }
        
        //[UnityTest]
        public IEnumerator ObliqueChunkRotation()
        {
            yield return GameplayUtils.Load();

            var playerView = Object.FindObjectOfType<PlayerView>();
            
            yield return PlayerAssert.IsNotSetTargetRotation(1f);
            
            var oldForward = playerView.gameObject.transform.forward;
            
            yield return new WaitForSeconds(1f);
            
            yield return PlayerAssert.IsSetTargetRotation(10f);
            yield return new WaitForSeconds(2f);
            
            yield return PlayerAssert.IsNotSetTargetRotation(3f);
            
            var currentForward = playerView.gameObject.transform.forward;
            Assert.AreNotEqual(oldForward, currentForward);
            
            yield return new WaitForSeconds(1f);
            
            oldForward = playerView.gameObject.transform.forward;
            
            yield return PlayerAssert.IsSetTargetRotation(10f);
            yield return new WaitForSeconds(1f);
            
            yield return PlayerAssert.IsNotSetTargetRotation(3f);
            
            currentForward = playerView.gameObject.transform.forward;
            Assert.AreNotEqual(oldForward, currentForward);
            
            yield return null;
        }
    }
}