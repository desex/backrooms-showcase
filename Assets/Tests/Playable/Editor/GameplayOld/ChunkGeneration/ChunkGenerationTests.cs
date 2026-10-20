using System.Collections;
using Db.GameLevelsSettings;
using Db.GameLevelsSettings.Impls.Core.GenerationSettings;
using Game.Utils.Chunks;
using NUnit.Framework;
using Tests.Playable.Gameplay;
using Tests.Playable.Utils;
using UnityEngine;
using ZenjectUtil.Test.Extensions;

namespace Tests.Playable.GameplayOld.ChunkGeneration
{
    public class ChunkGenerationTests : AIntegrationTest
    {
        private const string TEST_SCENE = "Generation_TestScene";
        
        private const string STRAIGHT_CHUNKS_SETTINGS = "ChunkGenerationSettingsForOnlyStraightTest";
        private const string TURN_LEFT_CHUNKS_SETTINGS = "ChunkGenerationSettingsForOnlyTurnLeftTest";
        private const string TURN_RIGHT_CHUNKS_SETTINGS = "ChunkGenerationSettingsForOnlyTurnRightTest";

        private const string ONLY_STRAIGHT_WALLS_CHUNKS_SETTINGS = "OnlyStraightWallsChunkGenerationSettings";
        private const string ONLY_LEDGE_WALLS_CHUNKS_SETTINGS = "OnlyLedgeWallsChunkGenerationSettings";
        private const string ONLY_NICHE_WALLS_CHUNKS_SETTINGS = "OnlyNicheWallsChunkGenerationSettings";
        
        private const string SCREAMER_WALLS_CHUNKS_SETTINGS = "ScreamersWallsGenerationSettings";

        private const string ONLY_SHOW_AND_HIDE_LAMP_SETTINGS = "ChunkGenerationSettingsForOnlyShowAndHideLampTest";

        protected override void SubstituteResources()
        {
            //ScenesBase.SetScene(TEST_SCENE);
        }

        [Category(TestCategoriesNames.GENERATION)]
        [Category(TestCategoriesNames.CHUNKS)]
        public IEnumerator ShowAndHideLampsTest()
        {
            var settings = Resources.Load<ChunkGenerationSettings>(ONLY_SHOW_AND_HIDE_LAMP_SETTINGS);
            ResourceSubstitute.ReplaceSubstitute<IChunkGenerationSettings>(settings);
            
            yield return GameplayUtils.Load();
            
            yield return new WaitForSeconds(2f);
            
            GameplayAssert.HasInactiveLamps(true);
            
            yield return new WaitForSeconds(2f);
            
            GameplayUtils.HideAllLamps();

            yield return new WaitForSeconds(0.3f);
            
            GameplayAssert.AllLampsInactive();
            
            yield return new WaitForSeconds(2f);
            
            GameplayUtils.ShowAllLamps();
            GameplayAssert.HasInactiveLamps(false);

            yield return new WaitForSeconds(2f);
        }     
        
        [Category(TestCategoriesNames.GENERATION)]
        [Category(TestCategoriesNames.CHUNKS)]
        public IEnumerator OnlyStraightChunksTest()
        {
            var settings = Resources.Load<ChunkGenerationSettings>(STRAIGHT_CHUNKS_SETTINGS);
            ResourceSubstitute.ReplaceSubstitute<IChunkGenerationSettings>(settings);
            
            yield return GameplayUtils.Load();
            
            yield return new WaitForSeconds(3f);
            
            GameplayAssert.AllChunksAreType(EChunkType.Straight);

            yield return new WaitForSeconds(3f);
        }

        [Category(TestCategoriesNames.GENERATION)]
        [Category(TestCategoriesNames.CHUNKS)]
        public IEnumerator OnlyTurnLeftChunksTest()
        {
            var settings = Resources.Load<ChunkGenerationSettings>(TURN_LEFT_CHUNKS_SETTINGS);
            ResourceSubstitute.ReplaceSubstitute<IChunkGenerationSettings>(settings);
            
            yield return GameplayUtils.Load();

            yield return PlayerAssert.IsReadyToTurn();
            
            yield return new WaitForSeconds(0.5f);

            yield return GameplayUtils.MakeTurnLeft();
            
            yield return PlayerAssert.IsReadyToTurn();
            
            yield return new WaitForSeconds(0.5f);

            yield return GameplayUtils.MakeTurnLeft();
            
            GameplayAssert.AllChunksAreType(EChunkType.TurnLeft);

            yield return new WaitForSeconds(0.5f);
        }
        
        [Category(TestCategoriesNames.GENERATION)]
        [Category(TestCategoriesNames.CHUNKS)]
        public IEnumerator OnlyTurnRightChunksTest()
        {
            var settings = Resources.Load<ChunkGenerationSettings>(TURN_RIGHT_CHUNKS_SETTINGS);
            ResourceSubstitute.ReplaceSubstitute<IChunkGenerationSettings>(settings);
            
            yield return GameplayUtils.Load();
            
            yield return PlayerAssert.IsReadyToTurn();
            
            yield return new WaitForSeconds(0.5f);

            yield return GameplayUtils.MakeTurnRight();
            
            yield return PlayerAssert.IsReadyToTurn();
            
            yield return new WaitForSeconds(0.5f);

            yield return GameplayUtils.MakeTurnRight();
            
            GameplayAssert.AllChunksAreType(EChunkType.TurnRight);

            yield return new WaitForSeconds(0.5f);
        }

        [Category(TestCategoriesNames.GENERATION)]
        [Category(TestCategoriesNames.CHUNKS)]
        public IEnumerator OnlyStraightWallsGenerationTest()
        {
            var settings = Resources.Load<ChunkGenerationSettings>(ONLY_STRAIGHT_WALLS_CHUNKS_SETTINGS);
            ResourceSubstitute.ReplaceSubstitute<IChunkGenerationSettings>(settings);
            
            yield return GameplayUtils.Load();
            
            yield return new WaitForSeconds(5f);
            
            GameplayAssert.AllWallsAreType(EChunkWallType.Straight);
            
            yield return new WaitForSeconds(2f);
        }
        
        [Category(TestCategoriesNames.GENERATION)]
        [Category(TestCategoriesNames.CHUNKS)]
        public IEnumerator OnlyLedgeWallsGenerationTest()
        {
            var settings = Resources.Load<ChunkGenerationSettings>(ONLY_LEDGE_WALLS_CHUNKS_SETTINGS);
            ResourceSubstitute.ReplaceSubstitute<IChunkGenerationSettings>(settings);
            
            yield return GameplayUtils.Load();
            
            yield return new WaitForSeconds(5f);
            
            GameplayAssert.AllWallsAreType(EChunkWallType.Ledge);
            
            yield return new WaitForSeconds(2f);
        }
        
        [Category(TestCategoriesNames.GENERATION)]
        [Category(TestCategoriesNames.CHUNKS)]
        public IEnumerator OnlyNicheWallsGenerationTest()
        {
            var settings = Resources.Load<ChunkGenerationSettings>(ONLY_NICHE_WALLS_CHUNKS_SETTINGS);
            ResourceSubstitute.ReplaceSubstitute<IChunkGenerationSettings>(settings);
            
            yield return GameplayUtils.Load();
            
            yield return new WaitForSeconds(5f);
            
            GameplayAssert.AllWallsAreType(EChunkWallType.Niche);
            
            yield return new WaitForSeconds(2f);
        }

        [Category(TestCategoriesNames.GENERATION)]
        [Category(TestCategoriesNames.CHUNKS)]
        public IEnumerator ScreamerChunkSpawnTest()
        {
            var settings = Resources.Load<ChunkGenerationSettings>(SCREAMER_WALLS_CHUNKS_SETTINGS);
            ResourceSubstitute.ReplaceSubstitute<IChunkGenerationSettings>(settings);
            
            yield return GameplayUtils.Load();
            
            yield return new WaitForSeconds(5f);
        }
    }
}
