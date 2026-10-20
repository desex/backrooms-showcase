using System.Collections;
using Game.Services.Chaser;
using Game.UI.Revive;
using NUnit.Framework;
using Tests.Helpers.Mock;
using Tests.Playable.Utils;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using ZenjectUtil.Test.Extensions;

namespace Tests.Playable.Gameplay.Lose
{
    public class ReviveTest : AIntegrationTest
    {
        private const string NICHES_CHUNK_GENERATION = "Niches_ChunkGenerationSettings";
        private const string NICHES_SEQUENCE_GENERATION = "Niches_SequenceGenerationSettings";
        
        protected override void SubstituteResources()
        {
            var chaserPresentationService = new Mock.ChaserPresentationService();
            ResourceSubstitute.ReplaceSubstitute<IChaserPresentationService>(chaserPresentationService);
        }
        
        [UnityTest]
        [Category(TestCategoriesNames.REVIVE)]
        public IEnumerator PlayerRevive_IsReviveToSpawnPoint_AfterDeathInNiche()
        {
            SetSequenceGenerationSettings(NICHES_SEQUENCE_GENERATION);
            SetGenerationSettings(NICHES_CHUNK_GENERATION);

            yield return GameplayUtils.Load();
            yield return new WaitForSeconds(2f);
            yield return GameplayUtils.StartSmoothSwipe(Vector2.left);
            yield return PlayerAssert.IsDead(20);
            yield return GameplayUtils.StopSwipe();
            yield return new WaitForSeconds(2f);
            var view = Object.FindObjectOfType<ReviveView>();
            var adButton = view.ReviveForAdButton.GetComponent<Button>();
            adButton.onClick.Invoke();//TODO: can check this cuz has error when start test but must work OK
            yield return new WaitForSeconds(3f);
            yield return PlayerAssert.IsNotDamaged();
        }
    }
}