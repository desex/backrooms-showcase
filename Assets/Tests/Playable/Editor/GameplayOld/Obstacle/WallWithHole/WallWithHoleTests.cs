using System.Collections;
using Db.GameLevelsSettings;
using Db.GameLevelsSettings.Impls.Core.GenerationSettings;
using Game.Services.Chaser;
using Game.Services.Providers;
using Game.Utils.Movement;
using NUnit.Framework;
using Tests.Helpers.Mock;
using Tests.Playable.Gameplay;
using Tests.Playable.UITest;
using Tests.Playable.Utils;
using UnityEngine;
using ZenjectUtil.Test.Extensions;

namespace Tests.Playable.GameplayOld.Obstacle.WallWithHole
{
    public class WallWithHoleTests : UiTest
    {
        private const string WALL_WITH_LEFT_HOLE_GENERATION_SETTINGS = "WallWithHoleLeft_GenerationSettings";
        
        protected override void SubstituteCommonResources()
        {
        }

        protected override void SubstituteResources()
        {
            var chunkGenerationSettings = Resources.Load<ChunkGenerationSettings>(WALL_WITH_LEFT_HOLE_GENERATION_SETTINGS);
            var chaserServiceMock = new Mock.ChaserPresentationService();
            var generationSettingsProvider = new Mock.GenerationResourcesProvider
            {
                ChunkGenerationSettings = chunkGenerationSettings,
            };
            ResourceSubstitute.AddSubstitute<IChunkGenerationSettings>(chunkGenerationSettings);
            ResourceSubstitute.AddSubstitute<IChaserPresentationService>(chaserServiceMock);
            ResourceSubstitute.AddSubstitute<IGenerationResourcesProvider>(generationSettingsProvider);
        }

        [Category(TestCategoriesNames.OBSTACLES)]
        //[Category(TestCategoriesNames.VISUAL)]
        public IEnumerator WallWithLeftHole_CorrectParticles()
        {
            yield return GameplayUtils.Load();

            yield return PlayerAssert.ReadyToPassObstacle(EObstaclePassType.JumpInWallWithHole);

            yield return GameplayUtils.Swipe(Vector2.up);

            yield return new WaitForSeconds(2f);
        }
    }
}