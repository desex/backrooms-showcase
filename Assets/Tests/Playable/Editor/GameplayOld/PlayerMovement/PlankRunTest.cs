using System.Collections;
using Db.Score;
using Game.Utils.Movement;
using Tests.Helpers.Mock;
using Tests.Playable.Utils;
using UnityEngine;
using UnityEngine.TestTools;
using ZenjectUtil.Test.Extensions;

namespace Tests.Playable.Gameplay.PlayerMovement
{
    public class PlankRunTest : AIntegrationTest
    {
        private const string TEST_SCENE = "PlankRun_TestScene";
        
        protected override void SubstituteResources()
        {
            //ScenesBase.SetScene(TEST_SCENE);
            
            var scoreParameters = new Mock.ScoreParameters();
            ResourceSubstitute.AddSubstitute<IScoreParameters>(scoreParameters);
        }

        //[UnityTest]
        public IEnumerator PlankRun()
        {
            yield return GameplayUtils.Load();
            
            yield return PlayerAssert.SlowingDownRatio(0f);
            yield return PlayerAssert.IsNeedSlowDown(false);

            yield return GameplayUtils.SmoothSwipe(Vector2.right, 2f, 0.05f);

            yield return PlayerAssert.IsPlankRunStart();
            
            yield return PlayerAssert.IsNeedSlowDown(true);

            yield return PlayerAssert.IsPlankRunInProgress();
            yield return PlayerAssert.SlowingDownRatio(0.2f);

            yield return PlayerAssert.IsPlankRunEnd();
            
            yield return PlayerAssert.IsNeedSlowDown(false);
            yield return PlayerAssert.SlowingDownRatio(0f);
            
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Run);
            yield return GameplayUtils.SmoothSwipe(Vector2.right, 2f);
            
            PlayerAssert.Score(1);

            yield return new WaitForSeconds(2f);
        }
    }
}