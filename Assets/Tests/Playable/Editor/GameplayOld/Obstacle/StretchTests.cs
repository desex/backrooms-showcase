using System.Collections;
using Db.Player;
using Game.Utils.Movement;
using ModestTree;
using Tests.Helpers.Mock;
using Tests.Playable.Utils;
using UnityEngine;
using UnityEngine.TestTools;
using ZenjectUtil.Test.Extensions;

namespace Tests.Playable.Gameplay.Obstacle
{
    public class StretchTests : AIntegrationTest
    {
        private const string STRETCH_TEST_SCENE = "PipeStretch_TestScene";

        protected override void SubstituteResources()
        {
            var playerHealthParametersBase = new Mock.PlayerHealthParametersBase
            {
                PlayerMaxHealth = 4,
                TimeBetweenInjuries = 999f,
            };
            
            ResourceSubstitute.AddSubstitute<IPlayerHealthParametersBase>(playerHealthParametersBase);
        }

        //[UnityTest]
        public IEnumerator VisualPipesAndColumnsStretching()
        {
            Assert.IsEqual(true, false); //TODO: delete this when will be repair tests
            //ScenesBase.SetScene(STRETCH_TEST_SCENE);
            
            yield return GameplayUtils.Load();
            
            for (var i = 0; i < 2; i++)
            {
                //yield return PlayerAssert.IsReadyToMonkeyJump(); //TODO: fix it when will be repair tests

                yield return GameplayUtils.Swipe(Vector2.up * 80);
                //yield return PlayerAssert.IsInMonkeyJump(); //TODO: fix it when will be repair tests

                yield return GameplayUtils.Swipe(Vector2.down * 80); 
                yield return PlayerAssert.BodyMoveType(EBodyMoveType.LongSlide);;
            }

            yield return new WaitForSeconds(3f);
        }
    }
}