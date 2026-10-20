using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;
using Game.DaoData;
using Game.UI.Death;
using Game.UI.Revive;
using NUnit.Framework;
using PdUtils.Dao;
using Tests.Helpers.Mock;
using Tests.Playable.Utils;
using TMPro;
using UnityEngine;
using ZenjectUtil.Test.Extensions;
using Object = UnityEngine.Object;

namespace Tests.Playable.Gameplay.UI
{
    public class DeathViewTest : AIntegrationTest
    {
        private const string TEST_SCENE = "ReviveView_TestScene";
        
        protected override void SubstituteResources()
        {
            //ScenesBase.SetScene(TEST_SCENE);
            
            var scoreDao = new Mock.Dao<LevelScoresData>();
            scoreDao.Save(new LevelScoresData
            {
                BestPassedDistance = 15, 
                BestScore = 1
            });
            ResourceSubstitute.AddSubstitute<IDao<LevelScoresData>>(scoreDao);
        }

        [Category(TestCategoriesNames.UI)]
        //[Category(TestCategoriesNames.DEATH_VIEW)]
        public IEnumerator ScoresAdd()
        {
            yield return GameplayUtils.Load();

            yield return new WaitForSeconds(2f);

            yield return FastlyPassThrowObstacle();

            yield return PlayerAssert.IsDead();
            
            yield return AssertUiViewActive<ReviveView>();
            
            var bestScore = GameplayUtils.GetBestDistance();
            
            yield return AssertUiViewActive<DeathView>(timeout: 10f);

            var passedDistanceText = Object.FindObjectsOfType<TMP_Text>().First(x => x.gameObject.name.Contains("Passed"));

            var passedDistance = float.Parse(Regex.Match(passedDistanceText.text, @"\d+").Value);

            Assert.GreaterOrEqual(bestScore, passedDistance);
        }
        
        private static IEnumerator FastlyPassThrowObstacle()
        { 
            ModestTree.Assert.IsEqual(true, false); //TODO: delete this when will be repair tests
            //yield return PlayerAssert.IsReadyToMonkeyJump(); //TODO: fix it when will be repair tests
            yield return PlayerAssert.IsReadyForFastActionMove();
            yield return GameplayUtils.Swipe(Vector2.up * 80);
            yield return PlayerAssert.IsInFastActionMove(2f);
            yield return PlayerAssert.IsNotReadyForFastActionMove();
        }
    }
}