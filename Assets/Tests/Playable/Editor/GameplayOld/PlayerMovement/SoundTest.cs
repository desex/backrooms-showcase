using System.Collections;
using Db.Score;
using Game.Utils;
using NUnit.Framework;
using Tests.Helpers.Mock;
using Tests.Playable.Utils;
using UnityEngine;
using UnityEngine.TestTools;
using ZenjectUtil.Test.Extensions;
using Assert = ModestTree.Assert;

namespace Tests.Playable.Gameplay.PlayerMovement
{
    public class SoundTest : AIntegrationTest
    {
        private const string SOUND_TEST_SCENE = "MonkeyJump_Test_Sound_Wood_Hit_TestScene";
        
        protected override void SubstituteResources()
        {
            var scoreParameters = new Mock.ScoreParameters();
            ResourceSubstitute.AddSubstitute<IScoreParameters>(scoreParameters);
        }
        
        //[UnityTest]
        [Category(TestCategoriesNames.SOUND)]
        public IEnumerator SoundWoodHitTest()
        {
            Assert.IsEqual(true, false); //TODO: delete this when will be repair tests
            //ScenesBase.SetScene(SOUND_TEST_SCENE);
            
            yield return GameplayUtils.Load();
            
            GameplayUtils.SetPlayerPosition(Vector3.zero);
            
            yield return PlayerAssert.IsGrounded();
            //yield return PlayerAssert.IsReadyToMonkeyJump(); //TODO: fix it when will be repair tests
            yield return PlayerAssert.IsDamaged();
            
            yield return new WaitForSeconds(1f);
        }
        
    }
}