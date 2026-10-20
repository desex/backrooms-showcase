using System.Collections;
using Db.GameLevelsSettings;
using Db.GameLevelsSettings.Impls.Core.GenerationSettings;
using Game.Services.Providers;
using NUnit.Framework;
using Tests.Helpers.Mock;
using Tests.Playable.Gameplay;
using Tests.Playable.UITest;
using Tests.Playable.Utils;
using UnityEngine;
using UnityEngine.TestTools;
using ZenjectUtil.Test.Extensions;

namespace Tests.Playable.GameplayOld.Obstacle
{
    public class ObstaclesCollisionTest : UiTest
    {
        private const string STRAIGHT_CORIDOR_WITH_HORIZONTAL_OBSTACLES = "StraightCoridorWithHorizontalObstacles";

        private Mock.GenerationResourcesProvider _generationProvider;

        protected override void SubstituteCommonResources()
        {
        }

        protected override void SubstituteResources()
        {
            _generationProvider = new Mock.GenerationResourcesProvider();
        }

        //[UnityTest]
        [Category(TestCategoriesNames.OBSTACLES)]
        public IEnumerator NotDie_OnObstacleCollision()
        {
            var chunkGenerationSettings = Resources.Load<ChunkGenerationSettings>(STRAIGHT_CORIDOR_WITH_HORIZONTAL_OBSTACLES);
            ResourceSubstitute.AddSubstitute<IChunkGenerationSettings>(chunkGenerationSettings);

            _generationProvider.ChunkGenerationSettings = chunkGenerationSettings;
            ResourceSubstitute.AddSubstitute<IGenerationResourcesProvider>(_generationProvider);

            yield return GameplayUtils.Load();

            yield return new WaitForSeconds(1);

            var player = Contexts.SharedInstance.Game.PlayerEntity;

            player.IsDebugChaserDeath = false;

            yield return PlayerAssert.IsDamaged(20f);

            yield return new WaitForSeconds(5f);

            yield return PlayerAssert.IsNotDead();
        }
    }
}