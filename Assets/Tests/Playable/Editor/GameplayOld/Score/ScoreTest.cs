using System.Collections;
using Db.Player;
using Db.Score;
using Game.DaoData;
using Game.UI.FinalScore;
using Game.UI.Revive;
using ModestTree;
using PdUtils.Dao;
using Tests.Helpers.Mock;
using Tests.Playable.Utils;
using UnityEngine;
using ZenjectUtil.Test.Extensions;

namespace Tests.Playable.Gameplay.Score
{
    public class ScoreTest : AIntegrationTest
    {
        private const string TEST_SCENE = "Score_TestScene";
        
        private readonly Mock.ScoreParameters _scoreParameters = new();

        protected override void SubstituteResources()
        {
            ResourceSubstitute.AddSubstitute<IScoreParameters>(_scoreParameters);
            
            var scoreDao = new Mock.Dao<LevelScoresData>();
            ResourceSubstitute.AddSubstitute<IDao<LevelScoresData>>(scoreDao);

            var speedParameters = new Mock.PlayerBasicMovementParametersBase();
            ResourceSubstitute.AddSubstitute<IPlayerBasicMovementParametersBase>(speedParameters);
            

            //ScenesBase.SetScene(TEST_SCENE);
        }

        //[UnityTest]
        public IEnumerator ComboIncreaseAfterFastActions()
        {
            yield return GameplayUtils.Load();
            
            yield return new WaitForSeconds(0.5f);

            yield return StartCombo();
            
            yield return FastlyPassThrowObstacle();
            
            yield return new WaitForSeconds(0.5f);
            
            PlayerAssert.ComboMultiplier(3);
            PlayerAssert.Score(12);
            
            yield return FastlyPassThrowObstacle();
            
            yield return new WaitForSeconds(0.5f);
            
            PlayerAssert.ComboMultiplier(4);
            PlayerAssert.Score(20);
            
            yield return new WaitForSeconds(2f);
        }

        //[UnityTest]
        public IEnumerator ComboResetAfterSlowAction()
        {
            yield return GameplayUtils.Load();
            
            yield return new WaitForSeconds(0.5f);

            yield return StartCombo();

            yield return SlowlyPassThrowObstacle();

            yield return new WaitForSeconds(0.5f);
            
            PlayerAssert.ComboMultiplier(3);
            PlayerAssert.Score(12);
            
            yield return SlowlyPassThrowObstacle();

            yield return new WaitForSeconds(0.5f);
            
            PlayerAssert.ComboMultiplier(4);
            PlayerAssert.Score(20);
            
            yield return new WaitForSeconds(2f);
        }
        
        //[UnityTest]
        public IEnumerator ComboResetAfterTime()
        {
            yield return GameplayUtils.Load();
            
            yield return new WaitForSeconds(0.5f);

            yield return StartCombo();
            
            yield return new WaitForSeconds(0.2f);
            
            yield return GameplayUtils.SmoothSwipe(Vector2.right);

            yield return new WaitForSeconds(0.5f);
            
            PlayerAssert.ComboMultiplier(2);
            
            yield return new WaitForSeconds(0.5f + _scoreParameters.ResetMultiplierDelaySeconds);
            
            PlayerAssert.ComboMultiplier(1);
            
            yield return new WaitForSeconds(2f);
        }
        
        //[UnityTest]
        public IEnumerator ComboResetAfterTakingDamage()
        {
            yield return GameplayUtils.Load();
            
            yield return new WaitForSeconds(0.5f);

            yield return StartCombo();

            yield return new WaitForSeconds(0.5f);
            
            PlayerAssert.ComboMultiplier(2);

            yield return PlayerAssert.IsDamaged();

            yield return GameplayUtils.SmoothSwipe(Vector2.left, 6f, 2f);

            PlayerAssert.ComboMultiplier(1);

            yield return new WaitForSeconds(2f);
        }
        
        //[UnityTest]
        public IEnumerator ScoreAfterDeath()
        {
            _scoreParameters.ScoreForOnePassedUnit = 1;
            
            yield return GameplayUtils.Load();
            
            yield return new WaitForSeconds(0.5f);

            yield return FastlyPassThrowObstacle();
            
            yield return new WaitForSeconds(0.4f);
            
            yield return FastlyPassThrowObstacle();
            
            yield return new WaitForSeconds(0.2f);

            yield return SlowlyPassThrowObstacle();

            yield return new WaitForSeconds(0.5f);
            
            PlayerAssert.ComboMultiplier(3);

            yield return PlayerAssert.IsDead();
            
            yield return AssertUiViewActive<ReviveView>();
            
            yield return GameplayUtils.PressCancelReviveAfterDeath();

            yield return AssertUiViewActive<FinalScoreView>(timeout: 10f);

            yield return new WaitForSeconds(2f);
            
            var finalScoreView = Object.FindObjectOfType<FinalScoreView>();
            
            var score = int.Parse(finalScoreView.ScoreText.text.Replace("p", ""));
            var passedDistance = int.Parse(finalScoreView.PassedDistanceText.text.Replace("m", ""));
            var bestPassedDistance = int.Parse(finalScoreView.BestPassedDistanceText.text.Replace("m", ""));
            
            Assert.IsApproximately(42, passedDistance, 5f);
            Assert.IsApproximately(92, score, 5f);
            Assert.IsEqual(passedDistance, bestPassedDistance);

            yield return new WaitForSeconds(2f);

            yield return GameplayUtils.PressRestartAfterDeath();
            yield return GameplayUtils.PressStartAndWaitGameplay();
            
            yield return PlayerAssert.IsDead();
            
            yield return AssertUiViewActive<ReviveView>();
            
            yield return GameplayUtils.PressCancelReviveAfterDeath();

            yield return AssertUiViewActive<FinalScoreView>(timeout: 10f);

            var finalScoreViewAfterDeath = Object.FindObjectOfType<FinalScoreView>();
            var bestPassedDistanceAfterDeath = int.Parse(finalScoreViewAfterDeath.BestPassedDistanceText.text.Replace("m", ""));
            
            Assert.IsEqual(passedDistance, bestPassedDistanceAfterDeath);
            
            yield return new WaitForSeconds(2f);
        }
        
        private static IEnumerator StartCombo()
        {
            PlayerAssert.Score(0);
            PlayerAssert.ComboMultiplier(1);
            yield return FastlyPassThrowObstacle();
            
            PlayerAssert.Score(2);
            PlayerAssert.ComboMultiplier(1);
            
            yield return FastlyPassThrowObstacle();
            
            PlayerAssert.Score(6);
            PlayerAssert.ComboMultiplier(2);

            yield return new WaitForSeconds(0.2f);
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
        
        private static IEnumerator SlowlyPassThrowObstacle()
        {
            Assert.IsEqual(true, false); //TODO: delete this when will be repair tests
            //yield return PlayerAssert.IsReadyToMonkeyJump(); //TODO: fix it when will be repair tests
            yield return PlayerAssert.IsNotReadyForFastActionMove();
            yield return GameplayUtils.Swipe(Vector2.up * 80);
            yield return PlayerAssert.IsNotInFastActionMove();
            yield return PlayerAssert.IsNotReadyForFastActionMove();
        }
    }
}