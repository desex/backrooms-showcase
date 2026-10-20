using System.Collections;
using Db.GameLevelsSettings;
using Db.GameLevelsSettings.Impls.Core.GenerationSettings;
using Db.Player;
using NUnit.Framework;
using Tests.Helpers.Mock;
using Tests.Playable.Gameplay;
using Tests.Playable.Utils;
using UnityEngine;
using ZenjectUtil.Test.Extensions;

namespace Tests.Playable.GameplayOld.Render
{
    public sealed class RenderTests : AIntegrationTest
    {
        private const string TEST_SCENE = "Generation_TestScene";
        
        private const string TV_LINES_CHUNKS_SETTINGS = "TvLinesChunkGenerationSettings";
        
        protected override void SubstituteResources()
        {
            var playerBasicMovementParameters = new Mock.PlayerBasicMovementParametersBase()
            {
                Speed = 0f,
            };
            ResourceSubstitute.ReplaceSubstitute<IPlayerBasicMovementParametersBase>(playerBasicMovementParameters);
           
            var settings = Resources.Load<ChunkGenerationSettings>(TV_LINES_CHUNKS_SETTINGS);
            ResourceSubstitute.ReplaceSubstitute<IChunkGenerationSettings>(settings);
            
            //ScenesBase.SetScene(TEST_SCENE);
        }

        //[Category(TestCategoriesNames.VISUAL)]
        [Category(TestCategoriesNames.RENDER)]
        public IEnumerator TvLinesTest()
        {
            yield return GameplayUtils.Load();

            GameplayUtils.StartTvLinesImmediately();
            GameplayAssert.AssertTvLineOn();
            
            yield return new WaitForSeconds(2f);

            GameplayUtils.StopTvLinesImmediately();
            GameplayAssert.AssertTvLinesOff();

            yield return new WaitForSeconds(2f);
            
            GameplayUtils.StartTvLinesImmediately();
            GameplayAssert.AssertTvLineOn();
            
            yield return new WaitForSeconds(2f);
            
            GameplayUtils.StopTvLinesSmoothly();

            yield return new WaitForSeconds(5f);
            GameplayAssert.AssertTvLinesOff();

            GameplayUtils.StartTvLinesSmoothly();

            yield return new WaitForSeconds(4f);
            GameplayAssert.AssertTvLineOn();
            
            yield return new WaitForSeconds(1f);
        }
        
        //[Category(TestCategoriesNames.VISUAL)]
        [Category(TestCategoriesNames.RENDER)]
        public IEnumerator TvLinesCancellationTest()
        {
            yield return GameplayUtils.Load();

            GameplayUtils.StartTvLinesImmediately();
            GameplayAssert.AssertTvLineOn();
            
            yield return new WaitForSeconds(2f);

            GameplayUtils.StopTvLinesSmoothly();

            yield return new WaitForSeconds(2f);

            GameplayUtils.StartTvLinesSmoothly();

            yield return new WaitForSeconds(5f);
        }

        //[Category(TestCategoriesNames.VISUAL)]
        [Category(TestCategoriesNames.RENDER)]
        public IEnumerator PeriodGlitchesTest()
        {
            yield return GameplayUtils.Load();
            
            GameplayUtils.StartGlitchesImmediately();

            yield return new WaitForSeconds(10f);
            
            GameplayUtils.StopGlitchesImmediately();

            yield return new WaitForSeconds(3f);
        }

        //[Category(TestCategoriesNames.VISUAL)]
        [Category(TestCategoriesNames.RENDER)]
        public IEnumerator OneGlitchEffectTest()
        {
            yield return GameplayUtils.Load();
            
            yield return new WaitForSeconds(2f);
            
            GameplayUtils.ShowGlitches(5f, 1f);

            yield return new WaitForSeconds(8f);
        }
    }
}