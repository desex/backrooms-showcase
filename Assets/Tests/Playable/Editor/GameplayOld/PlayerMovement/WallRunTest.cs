using System.Collections;
using Db.Score;
using Game.Utils.Movement;
using NUnit.Framework;
using Tests.Helpers.Mock;
using Tests.Playable.Utils;
using UnityEngine;
using UnityEngine.TestTools;
using ZenjectUtil.Test.Extensions;

namespace Tests.Playable.Gameplay.PlayerMovement
{
    public class WallRunTest : AIntegrationTest
    {
        private const string TEST_SCENE = "WallRun_TestScene";
        private const string DARK_CHUNK_TEST_SCENE = "WallRunInDurkChunk_TestScene";

        protected override void SubstituteResources()
        {
            //ScenesBase.SetScene(TEST_SCENE);
            
            var scoreParameters = new Mock.ScoreParameters();
            ResourceSubstitute.AddSubstitute<IScoreParameters>(scoreParameters);
        }

        [Category(TestCategoriesNames.MOVEMENT)]
        //[Category(TestCategoriesNames.VISUAL)]
        public IEnumerator WallRunSuccess()
        {
            yield return GameplayUtils.Load();
            
            yield return new WaitForSeconds(1f);

            yield return GameplayUtils.SmoothSwipe(Vector2.left, 1f, 4f);
            
            yield return PlayerAssert.IsNotDead();
            
            yield return new WaitForSeconds(1f); 
        }
        
        [Category(TestCategoriesNames.MOVEMENT)]
        //[Category(TestCategoriesNames.VISUAL)]
        public IEnumerator WallRunFail()
        {
            yield return GameplayUtils.Load();
            
            yield return new WaitForSeconds(1f);

            yield return GameplayUtils.SmoothSwipe(Vector2.left, 1f, 2.5f);

            yield return PlayerAssert.IsDead();
            
            yield return new WaitForSeconds(1f); 
        }

        [Category(TestCategoriesNames.MOVEMENT)]
        //[Category(TestCategoriesNames.VISUAL)]
        public IEnumerator WallRunInDarkChunk()
        {
            //ScenesBase.SetScene(DARK_CHUNK_TEST_SCENE);
            
            yield return GameplayUtils.Load();
            
            yield return new WaitForSeconds(1f);
            
            yield return GameplayUtils.StartSmoothSwipe(Vector2.left);
            
            yield return new WaitForSeconds(2f);

            yield return PlayerAssert.BodyMoveType(EBodyMoveType.WallRun);
            
            yield return new WaitForSeconds(2f);

            yield return GameplayUtils.Load();
            
            yield return new WaitForSeconds(1f);
            
            yield return GameplayUtils.StartSmoothSwipe(Vector2.right);
            
            yield return new WaitForSeconds(2f);

            yield return PlayerAssert.BodyMoveType(EBodyMoveType.WallRun);
            
            yield return new WaitForSeconds(2f);
        }
    }
}