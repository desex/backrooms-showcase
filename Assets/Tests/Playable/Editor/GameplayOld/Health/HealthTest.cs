using System.Collections;
using System.Reflection;
using Db.GameLevelsSettings;
using Db.GameLevelsSettings.Impls.Core.GenerationSettings;
using Db.Player;
using Game.Services.Providers;
using Game.UI.ChaseProgressBar;
using NUnit.Framework;
using Tests.Helpers.Mock;
using Tests.Playable.Gameplay;
using Tests.Playable.UITest;
using Tests.Playable.Utils;
using UnityEngine;
using UnityEngine.UI;
using ZenjectUtil.Test.Extensions;

namespace Tests.Playable.GameplayOld.Health
{
    public class HealthTestOld : UiTest
    {
        private const string ONLY_TURN_AND_TCHUNKS_GENERATION = "OnlyTurnAndTChunksGeneration";
        private const string CGS_WITH_START_SEQUENCE = "ChunkGenerationSettingsWithStartSequence";
        private const string SC_SEQUENCE_WITH_COLUMNS_IN_ROW = "SequencesConnectionColumnsOnRightSide";

        private Mock.GenerationResourcesProvider _generationProvider;

        private Mock.PlayerHealthParametersBase _playerHealthParametersBase;

        protected override void SubstituteCommonResources()
        {
        }

        protected override void SubstituteResources()
        {
            _generationProvider = new Mock.GenerationResourcesProvider();

            _playerHealthParametersBase = new()
            {
                TimeBetweenInjuries = 1f,
                TimeBetweenDamages = 0.25f,
                PlayerMaxHealth = 2,
            };

            ResourceSubstitute.AddSubstitute<IPlayerHealthParametersBase>(_playerHealthParametersBase);
        }

        //[UnityTest]
        public IEnumerator ChaserCatchingUp_OnPlayerCrashing()
        {
            var chunkGenerationSettings = Resources.Load<ChunkGenerationSettings>(CGS_WITH_START_SEQUENCE);
            ResourceSubstitute.AddSubstitute<IChunkGenerationSettings>(chunkGenerationSettings);

            _generationProvider.ChunkGenerationSettings = chunkGenerationSettings;
            ResourceSubstitute.AddSubstitute<IGenerationResourcesProvider>(_generationProvider);

            yield return GameplayUtils.Load();

            yield return new WaitForSeconds(4f);

            yield return GameplayUtils.SmoothSwipe(Vector2.right, distance: 0.1f);

            yield return new WaitForSeconds(8f);

            var progressBarView = Object.FindObjectOfType<ChaserView>();
            var slider = (Slider)typeof(ChaserView)
                                .GetField("_progressBar", BindingFlags.NonPublic | BindingFlags.Instance)
                                .GetValue(progressBarView);

            yield return GameplayUtils.SmoothSwipe(Vector2.left, distance: 0.1f);

            yield return PlayerAssert.IsDamaged();

            var startDistance = slider.value;

            yield return GameplayUtils.SmoothSwipe(Vector2.left);

            yield return new WaitForSeconds(2.9f);

            var endDistance = slider.value;

            var difference = endDistance - startDistance;

            Assert.GreaterOrEqual(difference, 0.31f);

            Assert.LessOrEqual(difference, 0.35f);

            yield return new WaitForSeconds(1f);
        }

        //[UnityTest]
        [Category(TestCategoriesNames.CHUNKS)]
        public IEnumerator NotDie_OnTurnChunkCollision()
        {
            var chunkGenerationSettings = Resources.Load<ChunkGenerationSettings>(ONLY_TURN_AND_TCHUNKS_GENERATION);
            ResourceSubstitute.AddSubstitute<IChunkGenerationSettings>(chunkGenerationSettings);

            _generationProvider.ChunkGenerationSettings = chunkGenerationSettings;
            ResourceSubstitute.AddSubstitute<IGenerationResourcesProvider>(_generationProvider);

            yield return GameplayUtils.Load();

            yield return new WaitForSeconds(1);

            var player = Contexts.SharedInstance.Game.PlayerEntity;

            player.IsDebugChaserDeath = false;

            yield return new WaitForSeconds(10);

            yield return PlayerAssert.IsNotDead();
        }

        //[UnityTest]
        public IEnumerator PlayerHealthRegeneration()
        {
            yield return GameplayUtils.Load();
            
            yield return PlayerAssert.IsDamaged();
            
            yield return GameplayUtils.SmoothSwipe(Vector2.left);

            yield return PlayerAssert.IsNotDamaged();

            yield return PlayerAssert.IsNotDead();
            
            yield return new WaitForSeconds(3f);
        }

        //[UnityTest]
        public IEnumerator DeathByObstacleHit()
        {
            _playerHealthParametersBase.TimeBetweenInjuries = 999f;
            
            yield return GameplayUtils.Load();

            yield return new WaitForSeconds(0.5f);
            
            yield return PlayerAssert.IsDamaged();

            yield return PlayerAssert.IsNotDead();

            yield return PlayerAssert.IsDead();
            
            yield return new WaitForSeconds(1f);
        }

        //[UnityTest]
        public IEnumerator InstantDeathByObstacleHit()
        {
            yield return GameplayUtils.Load();

            yield return new WaitForSeconds(0.5f);
            
            yield return new WaitForSeconds(1.5f);

            yield return PlayerAssert.IsDead(2);

            yield return new WaitForSeconds(2f);
        }
    }
}