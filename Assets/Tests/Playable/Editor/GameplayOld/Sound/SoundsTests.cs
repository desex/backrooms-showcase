using System.Collections;
using Db.GameLevelsSettings;
using Db.GameLevelsSettings.Impls.Core.GenerationSettings;
using Db.GameLevelsSettings.Impls.Obstacles;
using Db.GenerationSettings;
using Game.Services.Providers;
using Game.Utils.Movement;
using Tests.Helpers.Mock;
using Tests.Playable.UITest;
using Tests.Playable.Utils;
using UnityEngine;
using ZenjectUtil.Test.Extensions;

namespace Tests.Playable.GameplayOld.Sound
{
    public class SoundsTests : UiTest
    {
        protected override void SubstituteCommonResources()
        {
        }
        
        protected override void SubstituteResources()
        {
            var chunkGenerationSettings = Resources.Load<ChunkGenerationSettings>("ChunkGenerationSettingsRunSoundAfterObstaclePass");
            ResourceSubstitute.AddSubstitute<IChunkGenerationSettings>(chunkGenerationSettings);
            
            var obstaclesBase = Resources.Load<ObstaclesBase>("ObstacleBaseRunSoundAfterObstaclePass");
            ResourceSubstitute.AddSubstitute<IObstaclesBase>(obstaclesBase);
         
            var generationProvider = new Mock.GenerationResourcesProvider
            {
                ChunkGenerationSettings = chunkGenerationSettings,
                ObstaclesBase = obstaclesBase,
            };
            ResourceSubstitute.AddSubstitute<IGenerationResourcesProvider>(generationProvider);
        }


        //[UnityTest]
        public IEnumerator RunSoundContinuePlay_AfterPassingObstaclePass()
        {
            yield return GameplayUtils.Load();

            yield return GameplayAssert.AssertEventEmitterIsPlaying("Run");

            yield return PassMonkeyJumpObstacle();

            yield return GameplayAssert.AssertEventEmitterIsPlaying("Run");
            
            yield return new WaitForSeconds(1f);
        }

        private static IEnumerator PassMonkeyJumpObstacle()
        {
            yield return PlayerAssert.ReadyToPassObstacle(EObstaclePassType.MonkeyJump, 20f);
            
            yield return new WaitForSeconds(0.1f);
            
            yield return GameplayUtils.Swipe(Vector2.up * 80);
            yield return PlayerAssert.NotReadyToPassObstacle();
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Run);
        }
    }
}