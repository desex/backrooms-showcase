using System.Collections;
using Db.Player;
using Db.SoundScreamer;
using Ecs.Game.Components.Timer;
using NUnit.Framework;
using Tests.Helpers.Mock;
using Tests.Playable.UITest.Utils;
using Tests.Playable.Utils;
using UnityEngine;
using UnityEngine.TestTools;
using ZenjectUtil.Test.Extensions;

namespace Tests.Playable.Gameplay.Sound.Screamers
{
    public class ScreamerVisualTest : AIntegrationTest
    {
        private const string TEST_SCENE = "PlankRun_TestScene";

        protected override void SubstituteResources()
        {
            var soundParameters = new Mock.ScreamerParametersBase();
            ResourceSubstitute.AddSubstitute<IScreamerParametersBase>(soundParameters);

            var playerMoveParams = new Mock.PlayerBasicMovementParametersBase
            {
                Speed = 0
            };
            ResourceSubstitute.AddSubstitute<IPlayerBasicMovementParametersBase>(playerMoveParams);
            
            //ScenesBase.SetScene(TEST_SCENE);
        }

        //[Category(TestCategoriesNames.VISUAL)]
        [Category(TestCategoriesNames.SCREAMERS)]
        public IEnumerator WhisperScreamer()
        {
            yield return GameplayUtils.Load();

            yield return new WaitForSeconds(1);

            var player = DiUtils.Container.Resolve<GameContext>().PlayerEntity;

            for (var i = 0; i < 60; i++)
            {
                yield return new WaitForSeconds(1);

                Debug.Log($"Timer {player.Timer.Value} | State {player.ScreamerPlayBackState.PlayBackStatus}");
            }
        }
    }
}