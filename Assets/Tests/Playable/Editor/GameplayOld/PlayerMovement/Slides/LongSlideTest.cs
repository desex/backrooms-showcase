using System.Collections;
using Db.Score;
using Game.Utils.Movement;
using NUnit.Framework;
using Tests.Helpers.Mock;
using Tests.Playable.Utils;
using UnityEngine;
using UnityEngine.TestTools;
using ZenjectUtil.Test.Extensions;

namespace Tests.Playable.Gameplay.PlayerMovement.Slides
{
    public class LongSlideTest : AIntegrationTest
    {
        private const string LONG_SLIDE_LONG_OBSTACLES_TEST_SCENE = "LongSlide_LongObstacles_TestScene";
        private const int SUCCESSFUL_SLIDES_COUNT = 2;

        protected override void SubstituteResources()
        {
            var scoreParameters = new Mock.ScoreParameters();
            ResourceSubstitute.AddSubstitute<IScoreParameters>(scoreParameters);
            
            //ScenesBase.SetScene(LONG_SLIDE_LONG_OBSTACLES_TEST_SCENE);
        }
        
        //[UnityTest]
        [Category(TestCategoriesNames.MOVEMENT)]
        public IEnumerator FastLongSlideSuccess()
        {
            yield return GameplayUtils.Load();

            for (var i = 0; i < SUCCESSFUL_SLIDES_COUNT; i++)
            {
                PlayerAssert.Score(i * 2);
                
                yield return PlayerAssert.IsGrounded();
                yield return PlayerAssert.IsReadyForFastActionMove();
                yield return GameplayUtils.MakeSlide();
                yield return PlayerAssert.BodyMoveType(EBodyMoveType.LongSlide);
                yield return PlayerAssert.IsInFastActionMove();
                yield return PlayerAssert.IsNotReadyForFastActionMove();
                yield return PlayerAssert.IsNotDead();
                yield return PlayerAssert.IsNotDamaged(1);
            }
            
            PlayerAssert.ComboMultiplier(2);
            PlayerAssert.Score(6);
            
            yield return new WaitForSeconds(2f);
        }

        //[UnityTest]
        [Category(TestCategoriesNames.MOVEMENT)]
        public IEnumerator NormalLongSlideSuccess()
        {
            yield return GameplayUtils.Load();

            for (var i = 0; i < SUCCESSFUL_SLIDES_COUNT; i++)
            {
                yield return PlayerAssert.IsGrounded();
                yield return PlayerAssert.IsNotReadyForFastActionMove();
                yield return GameplayUtils.MakeSlide();
                yield return PlayerAssert.BodyMoveType(EBodyMoveType.LongSlide);
                yield return PlayerAssert.IsNotInFastActionMove();
                yield return PlayerAssert.IsNotDead();
                yield return PlayerAssert.IsNotDamaged(1);
                
                PlayerAssert.Score(i + 1);
                PlayerAssert.ComboMultiplier(1);
            }

            yield return new WaitForSeconds(2f);
        }

        //[UnityTest]
        [Category(TestCategoriesNames.MOVEMENT)]
        public IEnumerator LongSlideFail()
        {
            yield return GameplayUtils.Load();

            yield return PlayerAssert.IsGrounded();
            yield return PlayerAssert.IsDamaged();
            
            yield return new WaitForSeconds(1f);
        }
    }
}