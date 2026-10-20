using System.Collections;
using Game.Utils.Obstacles;
using NUnit.Framework;
using Tests.Playable.Utils;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Playable.Gameplay.Obstacle
{
    public class ObstacleGenerationTests : AIntegrationTest
    {
        private const string TEST_SCENE = "Generation_TestScene";

        private const string ONLY_PIPES_CHUNKS_GENERATION_SETTINGS = "ObstacleGenerationSettingsForOnlyPipesTest";

        protected override string GenerationSettingsPath => ONLY_PIPES_CHUNKS_GENERATION_SETTINGS;

        protected override void SubstituteResources()
        {
            //ScenesBase.SetScene(TEST_SCENE);
        }

        //[UnityTest]
        [Category(TestCategoriesNames.GENERATION)]
        [Category(TestCategoriesNames.OBSTACLES)]
        public IEnumerator OnlyPipesObstacleGeneration()
        {
            yield return GameplayUtils.Load();
            
            yield return new WaitForSeconds(1.5f);
            
            GameplayAssert.AllObstaclesAreType(EObstacleType.VerticalPipe);

            yield return new WaitForSeconds(2f);
        }
    }
}
