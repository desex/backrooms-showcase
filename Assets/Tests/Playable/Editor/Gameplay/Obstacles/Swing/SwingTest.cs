using Game.Services.Chaser;
using Game.Utils.Movement;
using NUnit.Framework;
using System.Collections;
using Tests.Helpers.Mock;
using Tests.Playable.Utils;
using UnityEngine;
using UnityEngine.TestTools;
using ZenjectUtil.Test.Extensions;

namespace Tests.Playable.Gameplay.Obstacles.Swing
{
    public class SwingTest : AIntegrationTest
    {
        private const string DOUBLE_SWING_CHUNK_GENERATION = "DoubleSwingChunkGeneration";
        private const string DOUBLE_SWING_SEQUENCE_GENERATION = "DoubleSwing_SequenceGenerationSettings";
        protected override string GenerationSettingsPath => "SingleSwingChunkGeneration";
        protected override string SequenceGenerationSettingsPath => "SingleSwing_SequenceGenerationSettings";
        protected override string ObstaclesBasePath => "SwingObstaclesBase";

        protected override void SubstituteResources()
        {
            var chaserPresentationService = new Mock.ChaserPresentationService();
            ResourceSubstitute.ReplaceSubstitute<IChaserPresentationService>(chaserPresentationService);
        }

        [UnityTest]
        [Category(TestCategoriesNames.OBSTACLES)]
        public IEnumerator SingleSwingAnimation_IsAnimationSynchronizedWithMovement()
        {
            yield return GameplayUtils.Load();

            yield return GameplayUtils.SmoothSwipe(Vector2.left, 2f, 0.5f);

            yield return PlayerAssert.ReadyToPassAnyObstacle();
            
            yield return GameplayUtils.Swipe(Vector2.up * 3f);
            
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Swing);

            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Run);
        }

        [UnityTest]
        [Category(TestCategoriesNames.OBSTACLES)]
        public IEnumerator DoubleSwingAnimation_IsAnimationSynchronizedWithMovement()
        {
            SetSequenceGenerationSettings(DOUBLE_SWING_SEQUENCE_GENERATION);
            SetGenerationSettings(DOUBLE_SWING_CHUNK_GENERATION);
            
            yield return GameplayUtils.Load();

            yield return new WaitForSeconds(0.5f);

            yield return GameplayUtils.SmoothSwipe(Vector2.left, 2f, 0.75f);

            yield return PlayerAssert.ReadyToPassAnyObstacle(20f);

            yield return null;
            
            yield return GameplayUtils.Swipe(Vector2.up * 3f);
            
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Swing);

            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Run);
            
            yield return PlayerAssert.ReadyToPassAnyObstacle();
            
            yield return GameplayUtils.Swipe(Vector2.up * 3f);
            
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Swing);

            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Run);
        }

    }
}