using System.Collections;
using Db.Score;
using Game.DaoData;
using Game.UI.Combo;
using ModestTree;
using PdUtils.Dao;
using Tests.Helpers.Mock;
using Tests.Playable.Utils;
using UnityEngine;
using ZenjectUtil.Test.Extensions;

namespace Tests.Playable.Gameplay.Score
{
    public class ScoreAndObstaclesWithoutZonesTest : AIntegrationTest
    {
        private readonly Mock.ScoreParameters _scoreParameters = new();
        private const string PIPES_SCENE = "ScorePipes_TestScene";
        private const string MULTIPLE_PIPES_SCENE = "MultiplePipesCombo_TestScene";
        private const string HOLE_SCENE = "ScoreHole_TestScene";

        protected override void SubstituteResources()
        {
            ResourceSubstitute.AddSubstitute<IScoreParameters>(_scoreParameters);
            
            var scoreDao = new Mock.Dao<LevelScoresData>();
            ResourceSubstitute.AddSubstitute<IDao<LevelScoresData>>(scoreDao);
        }

        //[UnityTest]
        public IEnumerator ScoreIncreaseAfterHoleJump()
        {
            //ScenesBase.SetScene(HOLE_SCENE);
            
            yield return GameplayUtils.Load();
            
            yield return new WaitForSeconds(3f);

            for (var i = 0; i < 2; i++)
            {
                yield return GameplayUtils.Swipe(Vector2.up * 80);

                yield return new WaitForSeconds(2);

                PlayerAssert.Score(i * 2 + 1);
                
                yield return new WaitForSeconds(0.6f);
            }
            
            PlayerAssert.ComboMultiplier(2);

            yield return new WaitForSeconds(2);
        }
        
        //[UnityTest]
        public IEnumerator ScoreIncreaseAfterClosePipeRun()
        {
            //ScenesBase.SetScene(PIPES_SCENE);
            
            yield return GameplayUtils.Load();

            for (var i = 0; i < 2; i++)
            {
                yield return new WaitForSeconds(3f);

                yield return PlayerAssert.IsNotDamaged();

                PlayerAssert.Score(i * 2 + 1);
            }
            
            PlayerAssert.ComboMultiplier(2);

            yield return new WaitForSeconds(2);
        }
        
        //[UnityTest]
        public IEnumerator ComboCorrectlyIncreaseAfterClosePipeRun()
        {
            _scoreParameters.ResetMultiplierDelaySeconds = 2f;
            
            //ScenesBase.SetScene(MULTIPLE_PIPES_SCENE);
            
            yield return GameplayUtils.Load();

            yield return AssertUiViewActive<ComboView>();

            var comboView = Object.FindObjectOfType<ComboView>();

            yield return new WaitForSeconds(2);

            yield return CheckComboIncreasing(3);

            yield return new WaitForSeconds(3f);

            yield return CheckComboIncreasing(2);
            
            yield return PlayerAssert.WaitForComboMultiplier(2);

            yield return new WaitForSeconds(2);

            IEnumerator CheckComboIncreasing(int needComboCount)
            {
                yield return PlayerAssert.WaitForComboMultiplier(1);
                
                for (var expectedCombo = 2; expectedCombo <= needComboCount; expectedCombo++)
                {
                    yield return PlayerAssert.WaitForComboMultiplier(expectedCombo);
                    
                    var visibleCombo = int.Parse(comboView.ComboCountText.text.Replace("x", ""));
                    Assert.IsApproximately(expectedCombo, visibleCombo);
                }
            }
        }
    }
}