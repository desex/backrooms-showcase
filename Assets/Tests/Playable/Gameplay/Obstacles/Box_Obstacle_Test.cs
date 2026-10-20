#if UNITY_EDITOR
using System.Collections;
using Core.Utils;
using Game.Services.Chaser;
using PdUtils.PlayerPrefs;
using PdUtils.PlayerPrefs.Impl;
using Tests.Helpers.Mock;
using Tests.Playable.Utils;
using UnityEngine;
using UnityEngine.TestTools;
using ZenjectUtil.Test.Extensions;

namespace Tests.Playable.Gameplay.Obstacles
{
    public class Box_Obstacle_Test : AIntegrationTest
    {
        protected override string SequenceGenerationSettingsPath => "Test_Box_ObstacleGeneration_Pool";

        protected override void SubstituteResources()
        {
            var playerPrefs = new InMemoryPlayerPrefsManager();
            playerPrefs.SetValue(SingleValueSavePathKeys.WAS_TUTORIAL_PASSED, true);
            ResourceSubstitute.AddSubstitute<IPlayerPrefsManager>(playerPrefs);
            
            var chaserPresentationService = new Mock.ChaserPresentationService();
            ResourceSubstitute.ReplaceSubstitute<IChaserPresentationService>(chaserPresentationService);
        }
        
        [UnityTest]
        public IEnumerator HitObstacle()
        {
            yield return GameplayUtils.Load();
            
            yield return new WaitForSeconds(10);
        }
    }
}
#endif