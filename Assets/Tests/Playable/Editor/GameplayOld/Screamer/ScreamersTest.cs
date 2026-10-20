using System.Collections;
using Db.GameLevelsSettings;
using Db.GameLevelsSettings.Impls.Core.GenerationSettings;
using NUnit.Framework;
using Tests.Playable.Gameplay;
using Tests.Playable.Utils;
using UnityEngine;
using ZenjectUtil.Test.Extensions;

namespace Tests.Playable.GameplayOld.Screamer
{
    public class ScreamersTest : AIntegrationTest
    {
        private const string TEST_SCENE = "BodyScreamer_TestScene";
        
        private const string ONLY_RIGHT_BODY_SCREAMER_CHUNK = "ChunkGenerationSettingsForOnlyTurnRightScreamerTest";
        private const string ONLY_LEFT_BODY_SCREAMER_CHUNK = "ChunkGenerationSettingsForOnlyTurnLeftScreamerTest";
        private const string ONLY_T_BODY_SCREAMER_CHUNK = "ChunkGenerationSettingsForOnlyTurnBothSidesScreamerTest";
        private const string ONLY_MONSTER_SCREAMER_CHUNK = "MonsterScreamerTest_ChunkGenerationSettings";
        private const string ONLY_HAND_SCREAMER_SETTINGS = "ChunkGenerationSettingsForOnlyHandScreamer";
        
        protected override void SubstituteResources()
        {
            //ScenesBase.SetScene(TEST_SCENE);
        }

        //[UnityTest]
        [Category(TestCategoriesNames.SCREAMERS)]
        public IEnumerator RightBodyScreamer()
        {
            var settings = Resources.Load<ChunkGenerationSettings>(ONLY_RIGHT_BODY_SCREAMER_CHUNK);
            ResourceSubstitute.ReplaceSubstitute<IChunkGenerationSettings>(settings);
            
            yield return GameplayUtils.Load();
            yield return new WaitForSeconds(4f);

            yield return PlayerAssert.IsReadyToTurn();

            yield return new WaitForSeconds(0.6f);
            
            yield return GameplayUtils.MakeTurnRight();
            
            yield return PlayerAssert.IsNotReadyToTurn();
        }

        //[UnityTest]
        [Category(TestCategoriesNames.SCREAMERS)]
        public IEnumerator LeftBodyScreamer()
        {
            var settings = Resources.Load<ChunkGenerationSettings>(ONLY_LEFT_BODY_SCREAMER_CHUNK);
            ResourceSubstitute.ReplaceSubstitute<IChunkGenerationSettings>(settings);
            
            yield return GameplayUtils.Load();
            
            yield return PlayerAssert.IsReadyToTurn();
            
            yield return new WaitForSeconds(0.3f);
            
            yield return GameplayUtils.MakeTurnLeft();
            
            yield return PlayerAssert.IsNotReadyToTurn();
        }

        //[UnityTest]
        [Category(TestCategoriesNames.SCREAMERS)]
        public IEnumerator TBodyScreamer()
        {
            var settings = Resources.Load<ChunkGenerationSettings>(ONLY_T_BODY_SCREAMER_CHUNK);
            ResourceSubstitute.ReplaceSubstitute<IChunkGenerationSettings>(settings);
            
            yield return GameplayUtils.Load();
            
            yield return PlayerAssert.IsReadyToTurn();

            yield return new WaitForSeconds(1f);

            yield return GameplayUtils.MakeTurnRight();
        }

        //[UnityTest]
        [Category(TestCategoriesNames.SCREAMERS)]
        public IEnumerator OnlyMonsterScreamerChunks()
        {
            var settings = Resources.Load<ChunkGenerationSettings>(ONLY_MONSTER_SCREAMER_CHUNK);
            ResourceSubstitute.ReplaceSubstitute<IChunkGenerationSettings>(settings);

            yield return GameplayUtils.Load();

            yield return new WaitForSeconds(15f);
        }
        
        //[UnityTest]
        [Category(TestCategoriesNames.SCREAMERS)]
        public IEnumerator HandScreamer()
        {
            var settings = Resources.Load<ChunkGenerationSettings>(ONLY_HAND_SCREAMER_SETTINGS);
            ResourceSubstitute.ReplaceSubstitute<IChunkGenerationSettings>(settings);
            
            yield return GameplayUtils.Load();

            yield return new WaitForSeconds(10f);
        }
    }
}