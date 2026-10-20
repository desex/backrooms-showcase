using System.Collections;
using Db.Player;
using Tests.Playable.UITest.Utils;
using Tests.Playable.Utils;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Playable.Gameplay.Chunks
{
    public class AutomaticTurnInCurvedChunk : AIntegrationTest
    {
        private const string CURVED_CHUNK_GENERATION = "CurvedChunk_ChunkGenerationSettings";
        private const string CURVED_SEQUENCE_GENERATION = "CurvedChunk_SequenceGenerationSettings";

        protected override string GenerationSettingsPath => CURVED_CHUNK_GENERATION;
        protected override string SequenceGenerationSettingsPath => CURVED_SEQUENCE_GENERATION;

        protected override void SubstituteResources()
        {
        }

        [UnityTest]
        public IEnumerator CanAutomaticTurn_InCurvedChunk_WithSwipe()
        {
            yield return GameplayUtils.Load();

            yield return ChangeSpeedMultiplier();
            
            yield return new WaitForSeconds(8f);
            yield return GameplayUtils.StartSmoothSwipe(Vector2.right);
            yield return PlayerAssert.IsNotDamaged(1f);
            yield return new WaitForSeconds(1f);
            yield return GameplayUtils.StopSwipe();
            yield return GameplayUtils.StartSmoothSwipe(Vector2.left);
            yield return PlayerAssert.IsNotDamaged(1f);
            yield return new WaitForSeconds(0.3f);
            yield return GameplayUtils.StopSwipe();
        }

        private static IEnumerator ChangeSpeedMultiplier()
        {
            var context = DiUtils.Container.Resolve<GameContext>();
            var parameters = DiUtils.Container.Resolve<IPlayerSpeedProgressionParameters>();
            var maxMultiplier = parameters.MaxProgressionMultiply;
            var player = context.PlayerEntity;
            player.ReplaceForwardSpeedMultiplier(maxMultiplier);

            yield return null;
        }
    }
}