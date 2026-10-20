using System.Collections;
using System.ComponentModel;
using Db.GameLevelsSettings;
using Db.GameLevelsSettings.Impls.Core.GenerationSettings;
using Db.Score;
using Game.Utils.Movement;
using Tests.Helpers.Mock;
using Tests.Playable.Gameplay;
using Tests.Playable.Utils;
using UnityEngine;
using ZenjectUtil.Test.Extensions;

namespace Tests.Playable.GameplayOld.PlayerMovement.Slides
{
    public class ControllableSlideTest : AIntegrationTest
    {
        private const string TEST_SCENE = "ControllableSlide_TestScene";
        private const string RAMP_TEST_SCENE = "SlideInRamp_TestScene";
        private const string RAMP_TEST_GENERATION = "RampAndStraightChunkGenerationSettings";

        protected override void SubstituteResources()
        {
            //ScenesBase.SetScene(TEST_SCENE);
            
            var scoreParameters = new Mock.ScoreParameters();
            ResourceSubstitute.AddSubstitute<IScoreParameters>(scoreParameters);
        }

        //[UnityTest]
        public IEnumerator SuccessSlide()
        {
            yield return GameplayUtils.Load();

            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Run);
            
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.ControllableSlide);
            
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Run);

            PlayerAssert.Score(1);
            
            yield return new WaitForSeconds(1f);
        }        
        
        //[UnityTest]
        public IEnumerator SlideGroundedCheck()
        {
            yield return GameplayUtils.Load();

            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Run);

            yield return PlayerAssert.BodyMoveType(EBodyMoveType.ControllableSlide);
            
            yield return new WaitForSeconds(0.5f);
            
            yield return PlayerAssert.IsGroundedDuringTime(3f);
            
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Run);
            
            yield return new WaitForSeconds(1f);
        }
        
        //[UnityTest]
        public IEnumerator SlideAfterJump()
        {
            yield return GameplayUtils.Load();

            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Run);
            
            yield return new WaitForSeconds(4f);

            yield return GameplayUtils.MakeJump();
            
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Jump);
            
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.ControllableSlide);
            
            yield return new WaitForSeconds(1f);
        }
        
        //[UnityTest]
        public IEnumerator JumpDuringSlide()
        {
            yield return GameplayUtils.Load();

            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Run);
            
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.ControllableSlide);
            
            yield return new WaitForSeconds(0.5f);
            
            yield return PlayerAssert.IsGrounded();
            yield return PlayerAssert.IsControllableSlideInterrupted(false);
            
            yield return GameplayUtils.Swipe(Vector2.up * 80);

            yield return PlayerAssert.IsNotGrounded();
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Jump);
            yield return PlayerAssert.IsControllableSlideInterrupted(true);
            
            yield return PlayerAssert.IsGrounded();
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.ControllableSlide);
            yield return PlayerAssert.IsControllableSlideInterrupted(true);
            
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Run);
            yield return PlayerAssert.IsControllableSlideInterrupted(false);

            PlayerAssert.Score(1);
            
            yield return new WaitForSeconds(1f);
        }

        //[UnityTest]
        [Category(TestCategoriesNames.MOVEMENT)]
        public IEnumerator RampSlide()
        {
            //ScenesBase.SetScene(RAMP_TEST_SCENE);
            
            var settings = Resources.Load<ChunkGenerationSettings>(RAMP_TEST_GENERATION);
            ResourceSubstitute.ReplaceSubstitute<IChunkGenerationSettings>(settings);
            
            yield return GameplayUtils.Load();
            
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Run);
            
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.ControllableSlide);
            
            yield return new WaitForSeconds(0.5f);
            
            yield return PlayerAssert.IsGrounded();

            yield return PlayerAssert.HasSlideDuration(false);
            
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Run);
            
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.ControllableSlide);

            yield return GameplayUtils.MakeJump();
            
            yield return new WaitForSeconds(0.5f);
            
            yield return PlayerAssert.IsGrounded();

            yield return PlayerAssert.HasSlideDuration(false);
        }
    }
}