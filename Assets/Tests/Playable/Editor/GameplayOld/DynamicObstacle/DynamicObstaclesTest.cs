using System.Collections;
using Db.GameLevelsSettings;
using Db.GameLevelsSettings.Impls.Core.GenerationSettings;
using Db.GameLevelsSettings.Impls.Obstacles;
using Db.GenerationSettings;
using Db.Player;
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

namespace Tests.Playable.GameplayOld.DynamicObstacle
{
    public class DynamicObstaclesTest : UiTest
    {
        private const string ONLY_TRAINS_GENERATION_SETTINGS = "GenerationSettings/OnlyTrainsGenerationSettings";
        private const string ONLY_HAND_GENERATION_SETTINGS = "GenerationSettings/OnlyHandGenerationSettings";

        private const string ONLY_STOP_TRAIN_OBSTACLES_BASE = "ObstaclesBase/OnlyStopTrainObstaclesBase";
        private const string ONLY_LEAVE_TRAIN_OBSTACLES_BASE = "ObstaclesBase/OnlyLeaveTrainObstaclesBase";
        private const string ONLY_TRAINS_FOR_CLIMB_GENERATION_SETTINGS = "GenerationSettings/OnlyTrainsForClimbGenerationSettings";
        
        private Mock.GenerationResourcesProvider _generationProvider;
        
        protected override void SubstituteCommonResources()
        {
        }

        protected override void SubstituteResources()
        {
            _generationProvider = new Mock.GenerationResourcesProvider();
            var chaserService = new Mock.ChaserPresentationService();
            ResourceSubstitute.ReplaceSubstitute<IChaserPresentationService>(chaserService);
        }

        //[UnityTest]
        [Category(TestCategoriesNames.DYNAMIC_OBSTACLES)]
        public IEnumerator HandDeath()
        {
            var settings = Resources.Load<ChunkGenerationSettings>(ONLY_HAND_GENERATION_SETTINGS);
            ResourceSubstitute.ReplaceSubstitute<IChunkGenerationSettings>(settings);
            
            yield return GameplayUtils.Load();

            yield return PlayerAssert.IsDead(20f);

            yield return GameplayUtils.Load();

            yield return new WaitForSeconds(2f);
            
            yield return PlayerAssert.IsDead();
        }

        //[UnityTest]
        [Category(TestCategoriesNames.DYNAMIC_OBSTACLES)]
        public IEnumerator TrainTriggerRadiusIsBigEnough()
        {
            var settings = Resources.Load<ChunkGenerationSettings>(ONLY_TRAINS_GENERATION_SETTINGS);
            ResourceSubstitute.ReplaceSubstitute<IChunkGenerationSettings>(settings);

            var speedProgressionSettings = new Mock.PlayerSpeedProgressionParameters()
            {
                InitialForwardSpeedMultiplier = 5f,
            };
            ResourceSubstitute.ReplaceSubstitute<IPlayerSpeedProgressionParameters>(speedProgressionSettings);

            yield return GameplayUtils.Load();
            
            yield return new WaitForSeconds(10f);

            yield return PlayerAssert.IsNotDead(1);
        }
        
        //[UnityTest]
        [Category(TestCategoriesNames.DYNAMIC_OBSTACLES)]
        public IEnumerator CanPass_StopTrain()
        {
            var chunkGenerationSettings = Resources.Load<ChunkGenerationSettings>(ONLY_TRAINS_GENERATION_SETTINGS);
            ResourceSubstitute.ReplaceSubstitute<IChunkGenerationSettings>(chunkGenerationSettings);
            
            var obstaclesBase = Resources.Load<ObstaclesBase>(ONLY_STOP_TRAIN_OBSTACLES_BASE);
            ResourceSubstitute.ReplaceSubstitute<IObstaclesBase>(obstaclesBase);
            
            yield return GameplayUtils.Load();

            yield return new WaitForSeconds(15f);
            
            yield return PlayerAssert.IsNotDamaged();

            yield return new WaitForSeconds(1f);
        }
        
        //[UnityTest]
        [Category(TestCategoriesNames.DYNAMIC_OBSTACLES)]
        public IEnumerator CanDamage_InStopTrain()
        {
            var chunkGenerationSettings = Resources.Load<ChunkGenerationSettings>(ONLY_TRAINS_GENERATION_SETTINGS);
            ResourceSubstitute.ReplaceSubstitute<IChunkGenerationSettings>(chunkGenerationSettings);
            
            var obstaclesBase = Resources.Load<ObstaclesBase>(ONLY_STOP_TRAIN_OBSTACLES_BASE);
            ResourceSubstitute.ReplaceSubstitute<IObstaclesBase>(obstaclesBase);
            
            yield return GameplayUtils.Load();

            yield return new WaitForSeconds(5f);
            
            yield return GameplayUtils.SmoothSwipe(Vector2.right);
            yield return PlayerAssert.IsDamaged();

            yield return new WaitForSeconds(1f);
        }

        //[UnityTest]
        [Category(TestCategoriesNames.DYNAMIC_OBSTACLES)]
        public IEnumerator CanDamage_FromPit_AfterLeavingTrain()
        {
            var chunkGenerationSettings = Resources.Load<ChunkGenerationSettings>(ONLY_TRAINS_GENERATION_SETTINGS);
            ResourceSubstitute.ReplaceSubstitute<IChunkGenerationSettings>(chunkGenerationSettings);     
            
            var obstaclesBase = Resources.Load<ObstaclesBase>(ONLY_LEAVE_TRAIN_OBSTACLES_BASE);
            ResourceSubstitute.ReplaceSubstitute<IObstaclesBase>(obstaclesBase);
            
            yield return GameplayUtils.Load();
            
            yield return new WaitForSeconds(15f);
            
            yield return PlayerAssert.IsDamaged();

            yield return new WaitForSeconds(1f);
        }    
        
        //[UnityTest]
        [Category(TestCategoriesNames.DYNAMIC_OBSTACLES)]
        public IEnumerator CanPass_LeavingTrain()
        {
            var chunkGenerationSettings = Resources.Load<ChunkGenerationSettings>(ONLY_TRAINS_GENERATION_SETTINGS);
            ResourceSubstitute.ReplaceSubstitute<IChunkGenerationSettings>(chunkGenerationSettings);     
            
            var obstaclesBase = Resources.Load<ObstaclesBase>(ONLY_LEAVE_TRAIN_OBSTACLES_BASE);
            ResourceSubstitute.ReplaceSubstitute<IObstaclesBase>(obstaclesBase);
            
            yield return GameplayUtils.Load();
            
            yield return new WaitForSeconds(8f);

            yield return GameplayUtils.MakeDoubleJump();
            
            yield return PlayerAssert.IsNotDamaged();

            yield return new WaitForSeconds(1f);
        }  
        
        //[UnityTest]
        [Category(TestCategoriesNames.DYNAMIC_OBSTACLES)]
        public IEnumerator CanPass_ClimbObstaclePass_TrainStop()
        {
            var chunkGenerationSettings = Resources.Load<ChunkGenerationSettings>(ONLY_TRAINS_FOR_CLIMB_GENERATION_SETTINGS);
            ResourceSubstitute.ReplaceSubstitute<IChunkGenerationSettings>(chunkGenerationSettings);   
            
            var obstaclesBase = Resources.Load<ObstaclesBase>(ONLY_STOP_TRAIN_OBSTACLES_BASE);
            ResourceSubstitute.ReplaceSubstitute<IObstaclesBase>(obstaclesBase);           
            
            _generationProvider.ObstaclesBase = obstaclesBase;
            _generationProvider.ChunkGenerationSettings = chunkGenerationSettings;
            ResourceSubstitute.ReplaceSubstitute<IGenerationResourcesProvider>(_generationProvider);
            
            yield return GameplayUtils.Load();
            yield return PlayerAssert.ReadyToPassObstacle(EObstaclePassType.Climb);
            yield return GameplayUtils.Swipe(Vector2.up);
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Run);
            yield return GameplayUtils.Swipe(Vector2.up);
            yield return null;
            yield return PlayerAssert.IsNotJumping();
            yield return new WaitForSeconds(1f);
        }  
        
        //[UnityTest]
        [Category(TestCategoriesNames.DYNAMIC_OBSTACLES)]
        public IEnumerator NotIncreaseCombo_AfterHandFromCeilingCollision()
        {
            var chunkGenerationSettings = Resources.Load<ChunkGenerationSettings>(ONLY_HAND_GENERATION_SETTINGS);
            ResourceSubstitute.AddSubstitute<IChunkGenerationSettings>(chunkGenerationSettings);

            _generationProvider.ChunkGenerationSettings = chunkGenerationSettings;
            ResourceSubstitute.AddSubstitute<IGenerationResourcesProvider>(_generationProvider);
            
            yield return GameplayUtils.Load();

            yield return PlayerAssert.IsDead(120f);
            
            yield return new WaitForSeconds(1f);

            PlayerAssert.ComboMultiplier(1f);
        }
    }
}