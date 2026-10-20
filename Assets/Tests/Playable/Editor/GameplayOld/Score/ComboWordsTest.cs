using System.Collections;
using Db.Player;
using Db.Score;
using Game.Utils.Obstacles;
using Tests.Helpers.Mock;
using Tests.Playable.Utils;
using UnityEngine;
using UnityEngine.TestTools;
using ZenjectUtil.Test.Extensions;

namespace Tests.Playable.Gameplay.Score
{
    public class ComboWordsTest : AIntegrationTest
    {
        private const string TEST_SCENE = "ObstaclesComboWords_TestScene";
        private const string MULTIPLE_PIPES_TEST_SCENE = "MultiplePipesComboWords_TestScene";

        protected override void SubstituteResources()
        {
            var scoreParameters = new Mock.ScoreParameters();
            ResourceSubstitute.AddSubstitute<IScoreParameters>(scoreParameters);

            var speedParameters = new Mock.PlayerBasicMovementParametersBase();
            ResourceSubstitute.AddSubstitute<IPlayerBasicMovementParametersBase>(speedParameters);
        }

        //[UnityTest]
        public IEnumerator ComboWordsResetAfterMonkeyJumpNearPipe()
        {
            //ScenesBase.SetScene(TEST_SCENE);
            
            yield return GameplayUtils.Load();
            
            yield return FastlyPassMonkeyJumpObstacle();
            
            yield return PlayerAssert.WaitForComboMultiplier(2f);
            
            yield return PlayerAssert.ComboWordType(EComboWordType.Default);
            
            yield return new WaitForSeconds(1.5f);
            
            yield return FastlyPassMonkeyJumpObstacle();
            
            yield return PlayerAssert.WaitForComboMultiplier(3f);
            
            yield return PlayerAssert.ComboWordType(EComboWordType.Default);

            yield return new WaitForSeconds(2f);
        }

        //[UnityTest]
        public IEnumerator ComboWordsDoNotChangeNearMultiplePipes()
        {
            //ScenesBase.SetScene(MULTIPLE_PIPES_TEST_SCENE);
            
            yield return GameplayUtils.Load();

            for (var i = 1; i <= 4; i++)
            {
                yield return PlayerAssert.WaitForComboMultiplier(i+1);
                yield return PlayerAssert.ComboWordType(EComboWordType.Column);
            }

            yield return new WaitForSeconds(2f);
        }
        
        private static IEnumerator FastlyPassMonkeyJumpObstacle()
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