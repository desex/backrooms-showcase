using System.Collections;
using Db.Player;
using NUnit.Framework;
using Tests.Helpers.Mock;
using Tests.Playable.Utils;
using UnityEngine;
using UnityEngine.TestTools;
using ZenjectUtil.Test.Extensions;

namespace Tests.Playable.Gameplay.PlayerMovement
{
    public class SpeedProgressionTest: AIntegrationTest
    {
        private Mock.PlayerSpeedProgressionParameters _playerSpeedProgressionParameters;
        private const string TEST_SCENE = "SpeedProgression_TestScene";
        
        protected override void SubstituteResources()
        {
            //ScenesBase.SetScene(TEST_SCENE);
            _playerSpeedProgressionParameters = new ();
            ResourceSubstitute.AddSubstitute<IPlayerSpeedProgressionParameters>(_playerSpeedProgressionParameters);
        }

        //[UnityTest]
        [Category(TestCategoriesNames.MOVEMENT)]
        public IEnumerator SpeedProgression()
        {
            yield return GameplayUtils.Load();

            yield return new WaitForSeconds(1f);

            yield return PlayerAssert.DistancePassed(Mathf.FloorToInt(_playerSpeedProgressionParameters.MetersProgression));

            yield return PlayerAssert.ForwardSpeedMultiplier(1 + _playerSpeedProgressionParameters.ProgressionMultiply);
        }
    }
}