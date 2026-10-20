using System.Collections;
using System.Text.RegularExpressions;
using Game.DaoData;
using Game.UI.Death;
using Game.UI.Revive;
using ModestTree;
using PdUtils.Dao;
using Tests.Helpers.Mock;
using Tests.Playable.Utils;
using UnityEngine;
using ZenjectUtil.Test.Extensions;
using Object = UnityEngine.Object;

namespace Tests.Playable.Gameplay.UI
{
    public class ReviveViewTest : AIntegrationTest
    {
        private const string TEST_SCENE = "ReviveView_TestScene";

        protected override void SubstituteResources()
        {
            //ScenesBase.SetScene(TEST_SCENE);
            
            var scoreDao = new Mock.Dao<LevelScoresData>();
            scoreDao.Save(new LevelScoresData(){BestPassedDistance = 1, BestScore = 1});
            ResourceSubstitute.AddSubstitute<IDao<LevelScoresData>>(scoreDao);
        }

        //[UnityTest]
        public IEnumerator ShowRestartWindowWithCounter()
        {
            yield return WaitForReviveView();

            yield return new WaitForSeconds(5f);

            yield return AssertUiViewActive<DeathView>();
        }

        //[UnityTest]
        public IEnumerator ShowRestartWindowWithButton()
        {
            yield return WaitForReviveView();
            
            yield return new WaitForSeconds(1f);
            
            yield return GameplayUtils.PressCancelReviveAfterDeath();
            
            yield return AssertUiViewActive<DeathView>();
        }

        //[UnityTest]
        public IEnumerator RecordTest()
        {
            yield return GameplayUtils.Load();

            yield return new WaitForSeconds(2f);

            yield return FastlyPassThrowObstacle();

            yield return PlayerAssert.IsDead();
            
            yield return AssertUiViewActive<ReviveView>();
            
            var bestDistance = GameplayUtils.GetBestDistance();
            var recordValueText = Regex.Match(Object.FindObjectOfType<ReviveView>().RemainingDistanceText.text, @"\d").Value;
            var recordValue = float.Parse(recordValueText);

            Assert.IsApproximately(bestDistance, recordValue,0.1f );
            
            yield return new WaitForSeconds(2f);

            yield return GameplayUtils.PressCancelReviveAfterDeath();
            yield return GameplayUtils.PressRestartAfterDeath();
            yield return GameplayUtils.PressStartAndWaitGameplay();

            yield return PlayerAssert.IsDead();
            
            yield return AssertUiViewActive<ReviveView>();
            
            yield return new WaitForSeconds(2f);
        }

        private IEnumerator WaitForReviveView()
        {
            yield return GameplayUtils.Load();
            
            yield return new WaitForSeconds(5f);

            yield return PlayerAssert.IsDead();
            
            yield return AssertUiViewActive<ReviveView>();
        }
        
        private static IEnumerator FastlyPassThrowObstacle()
        { 
            Assert.IsEqual(true, false); //TODO: delete this when will be repair tests
            //yield return PlayerAssert.IsReadyToMonkeyJump(); //TODO: fix it when will be repair tests
            yield return PlayerAssert.IsReadyForFastActionMove();
            yield return GameplayUtils.Swipe(Vector2.up * 80);
            yield return PlayerAssert.IsInFastActionMove(2f);
            yield return PlayerAssert.IsNotReadyForFastActionMove();
        }
    }
}