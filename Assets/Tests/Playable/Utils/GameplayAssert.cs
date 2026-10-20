using System.Collections;
using System.Linq;
using Ecs.Utils;
using Ecs.Views.Linkable.Impl.Chunk;
using Ecs.Views.Linkable.Impl.Obstacles;
using FMODUnity;
using Game.Utils.Chunks;
using Game.Utils.Obstacles;
using Game.Views.Lamps;
using NUnit.Framework;
using Tests.Playable.UITest.Custom.Conditions.Entity;
using Tests.Playable.UITest.Utils;
using UnityEngine;

namespace Tests.Playable.Utils
{
    public static class GameplayAssert
    {
        public static void AllChunksAreType(EChunkType chunkType)
        {
            var chunks = Object.FindObjectsOfType<ChunkView>();

            foreach (var chunk in chunks)
            {
                var uid = chunk.EntityId;
                var game = DiUtils.Container.Resolve<GameContext>();
                var chunkEntity = game.GetEntityWithUid(uid!.Value);
                var node = chunkEntity.ChunkNode.Value;
                var type = node.ChunkType;
                Assert.IsTrue(type == chunkType);
            }
        }

        public static void HasInactiveLamps(bool isActive)
        {
            var lamps = Object.FindObjectsOfType<LampView>(true);
            Assert.IsTrue(lamps.Any(lamp => lamp.gameObject.activeSelf == false) == isActive);
        }

        public static void AllLampsInactive()
        {
            var lamps = Object.FindObjectsOfType<LampView>(true);
            Assert.IsTrue(lamps.Any(lamp => lamp.gameObject.activeSelf) == false);
        }

        public static void AllWallsAreType(EChunkWallType wallType)
        {
            var walls = Object.FindObjectsOfType<ChunkWallView>();
            
            foreach (var wall in walls)
            {
                var uid = wall.EntityId;
                var game = DiUtils.Container.Resolve<GameContext>();
                var wallEntity = game.GetEntityWithUid(uid!.Value);
                var type = wallEntity.WallType.Value;
                Assert.IsTrue(type == wallType);
            }
        }

        public static void AllObstaclesAreType(EObstacleType obstacleType)
        {
            var obstacles = Object.FindObjectsOfType<ObstacleGroupView>();

            foreach (var obstacle in obstacles)
            {
                var uid = obstacle.EntityId;
                var game = DiUtils.Container.Resolve<GameContext>();
                var obstacleEntity = game.GetEntityWithUid(uid!.Value);

                if (!obstacleEntity.IsObstacleGroup)
                    continue;

                var thisType = obstacleEntity.ObstacleType.Value;
                
                if (thisType == EObstacleType.Wall)
                    continue;
                
                Assert.IsTrue(thisType == obstacleType);
            }
        }

        public static void AssertTvLineOn()
        {
            var tvLines = GameplayUtils.GetTvLines();
            
            Assert.IsTrue(tvLines.isActive);
            Assert.IsTrue(Mathf.Approximately(tvLines.Settings.ShaderStrength, 1f));
        }
        
        public static void AssertTvLinesOff()
        {
            var tvLines = GameplayUtils.GetTvLines();
            
            Assert.IsFalse(tvLines.isActive);
            Assert.IsTrue(Mathf.Approximately(tvLines.Settings.ShaderStrength, 0f));
        }
        
        public static IEnumerator AssertEventEmitterIsPlaying(string targetEventName, float timeout = 5f)
        {
#if UNITY_EDITOR
            yield return AssertUtils.WaitFor(() =>
                {
                    var allEmitters = Object.FindObjectsOfType<StudioEventEmitter>();
                    foreach (var studioEventEmitter in allEmitters)
                    {
                        var eventReferencePath = studioEventEmitter.EventReference.Path;
                        var eventName = eventReferencePath.Replace("event:/", "");
                        if (!eventName.Equals(targetEventName))
                            continue;

                        if (studioEventEmitter.IsPlaying())
                            return true;
                    }

                    return false;
                },
                timeout
            );
#else
            yield return null;
#endif
        }
        
        public static IEnumerator GameState(EGameState gameState, float timeOut = 10f)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                        .SetConditionName($"Game state is {gameState}")
                        .SetEntityFilter(entity => entity.HasGameState && entity.GameState.Value == gameState && !entity.IsDestroyed)
                        .SetCount(1)
                        .SetTimeout(10)
                        .Build();
        }
    }
}