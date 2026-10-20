using System.Collections;
using System.Reflection;
using Game.UI.ChaseProgressBar;
using NUnit.Framework;
using Tests.Playable.Utils;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests.Playable.Gameplay.Health
{
    public class HealthTest : AIntegrationTest
    {
        private const string ONLY_TURN_AND_TCHUNKS_GENERATION = "OnlyTurnAndTChunks_ChunkGenerationSettings";
        private const string ONLY_TURN_AND_TCHUNKS_SEQUENCE_GENERATION = "OnlyTurnAndTChunk_SequenceGenerationSettings";
        private const string WITH_START_SEQUENCE_GENERATION = "WithStartSequence_ChunkGenerationSettings";
        private const string WITH_START_SEQUENCE_SEQUENCE_GENERATION = "WithStartSequence_SequenceGenerationSettings";

        protected override void SubstituteResources()
        {
        }

        [UnityTest]
        public IEnumerator ChaserCatchingUp_OnPlayerCrashing()
        {
            SetSequenceGenerationSettings(WITH_START_SEQUENCE_SEQUENCE_GENERATION);
            SetGenerationSettings(WITH_START_SEQUENCE_GENERATION);

            yield return GameplayUtils.Load();

            yield return new WaitForSeconds(4f);

            yield return GameplayUtils.SmoothSwipe(Vector2.right, distance: 0.1f);

            yield return new WaitForSeconds(8f);

            var progressBarView = Object.FindObjectOfType<ChaserView>();
            var slider = typeof(ChaserView)
                        .GetField("_progressBar", BindingFlags.NonPublic | BindingFlags.Instance)!
                                .GetValue(progressBarView) as Slider;

            yield return GameplayUtils.SmoothSwipe(Vector2.left, distance: 0.1f);

            yield return PlayerAssert.IsDamaged();

            var startDistance = slider!.value;

            yield return new WaitForSeconds(3f);

            var endDistance = slider.value;

            var difference = endDistance - startDistance;

            Assert.GreaterOrEqual(difference, 0.31f);

            Assert.LessOrEqual(difference, 0.35f);

            yield return new WaitForSeconds(1f);
        }

        [UnityTest]
        [Category(TestCategoriesNames.CHUNKS)]
        public IEnumerator NotDie_OnTurnChunkCollision()
        {
            SetSequenceGenerationSettings(ONLY_TURN_AND_TCHUNKS_SEQUENCE_GENERATION);
            SetGenerationSettings(ONLY_TURN_AND_TCHUNKS_GENERATION);

            yield return GameplayUtils.Load();

            yield return new WaitForSeconds(1);

            var player = Contexts.SharedInstance.Game.PlayerEntity;

            player.IsDebugChaserDeath = false;

            yield return new WaitForSeconds(10);

            yield return PlayerAssert.IsNotDead();
        }

    }
}