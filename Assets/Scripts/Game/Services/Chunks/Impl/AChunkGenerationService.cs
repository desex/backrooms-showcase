using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Db.GameLevelsSettings.Impls.Sequence;
using Ecs.Managers;
using Game.Services.CheckPoints;
using Game.Services.DistanceCalculation;
using Game.Services.Providers;
using Game.Utils;
using Game.Utils.Chunks;
using Game.Utils.ColorsUtils;
using Game.Utils.DistanceCalculation;
using UnityEngine;

namespace Game.Services.Chunks.Impl
{
    public abstract class AChunkGenerationService : IChunkGenerationService
    {
        protected readonly ICheckpointService CheckpointService;
        protected readonly IChunkService ChunkService;
        protected readonly IDistanceCalculationService DistanceCalculationService;
        protected readonly IChunkSegmentService ChunkSegmentService;
        protected readonly GameContext Game;
        protected readonly IGenerationResourcesProvider GenerationResourcesProvider;

        protected AChunkGenerationService(
            IGenerationResourcesProvider generationResourcesProvider,
            IDistanceCalculationService distanceCalculationService,
            IChunkSegmentService chunkSegmentService,
            ICheckpointService checkpointService,
            IChunkService chunkService,
            GameContext game
        )
        {
            GenerationResourcesProvider = generationResourcesProvider;
            DistanceCalculationService = distanceCalculationService;
            ChunkSegmentService = chunkSegmentService;
            CheckpointService = checkpointService;
            ChunkService = chunkService;
            Game = game;
        }

        public Uid LastChunkUid { get; protected set; }

        public virtual void Initialize()
        {
        }

        public virtual void Reset()
        {
        }

        public virtual async UniTask GenerateFirstSequence(
            SequenceParameters parameters,
            Vector3 offset,
            CancellationToken token
        )
        {
            Debug.Assert(true, $"{nameof(AChunkGenerationService)} | Assign first sequence");
        }

        public virtual async UniTask GenerateNextChunk(ChunkNode node, CancellationToken token)
        {
            var entityId = node.ChunkUid;
            var chunkEntity = Game.GetEntityWithUid(entityId);

            var chunkType = chunkEntity.ChunkNode.Value.ChunkType;

            if (chunkEntity.ChunkNode.Value.IsGenerationStopped)
                return;

#if UNITY_EDITOR
            try
            {
                await CreateNextChunks(token);
            }
            catch (Exception e)
            {
                DebugUtility.Log(e.ToString(), UtilsColors.ErrorColor);
            }
#else
            await CreateNextChunks(token);
#endif

            return;

            async UniTask CreateNextChunks(CancellationToken token)
            {
                if (ChunkUtility.HasTwoAnchors(chunkType))
                    await GenerateNextChunk(
                        chunkEntity,
                        new[]
                        {
                            chunkEntity.ChunkAnchors.Value.EndLeftAnchor, chunkEntity.ChunkAnchors.Value.EndRightAnchor,
                        },
                        token
                    );
                else
                    await GenerateNextChunk(chunkEntity, chunkEntity.ChunkAnchors.Value.EndOnlyAnchor, token);
            }
        }

        protected abstract UniTask GenerateNextChunk(
            GameEntity chunkEntity,
            Transform endAnchor,
            CancellationToken token
        );

        protected virtual async UniTask GenerateNextChunk(
            GameEntity chunkEntity,
            Transform[] anchors,
            CancellationToken token
        )
        {
            await GenerateNextChunk(chunkEntity, anchors[0], token);
            await GenerateNextChunk(chunkEntity, anchors[1], token);
        }
    }
}