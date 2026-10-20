using System;
using System.Collections.Generic;
using Db.GameLevelsSettings.Impls.Sequence;
using Db.Score;
using Db.TransitionHoleChunk;
using Game.Services.DistanceCalculation;
using Game.Services.Providers;
using Game.Services.Random;
using Game.Utils;
using Game.Utils.Chunks;
using Game.Utils.Sequence;
using UnityEngine;
using UnityEngine.Pool;

namespace Game.Services.Chunks.Impl
{
    public class SequentialChunkPredictionService : ISequentialChunkPredictionService
    {
        private readonly GameContext _game;
        private readonly IChunkSegmentService _chunkSegmentService;
        private readonly IScoreParameters _scoreParameters;
        private readonly IGenerationResourcesProvider _generationResourcesProvider;
        private readonly IRandomService _randomService;
        private readonly ITransitionHoleParameters _transitionHoleParameters;

        public SequentialChunkPredictionService(
            GameContext game,
            IChunkSegmentService chunkSegmentService,
            IScoreParameters scoreParameters,
            IGenerationResourcesProvider generationResourcesProvider,
            IRandomService randomService,
            ITransitionHoleParameters transitionHoleParameters
        )
        {
            _game = game;
            _chunkSegmentService = chunkSegmentService;
            _scoreParameters = scoreParameters;
            _generationResourcesProvider = generationResourcesProvider;
            _randomService = randomService;
            _transitionHoleParameters = transitionHoleParameters;
        }

        public SequenceParameters ChooseSequence(ChunkNode node)
        {
            var lastTransitionDistance = _game.LevelEntity.LastTransitionDistance.Value;
            var lastSegment = _chunkSegmentService.LastChunkSegment;

            var currentDistance = lastSegment.EndDistance * _scoreParameters.DistanceToMetersRatio
                                  - lastTransitionDistance;

            var currentGroup = FindTargetDistanceGroup((int)currentDistance);

            if (currentGroup == null)
            {
                Debug.LogError($"{nameof(SequentialChunkGenerationService)} | Not found distance group");
                return null;
            }

            return SelectSequenceForGroup(currentGroup, node);
        }

        public SequenceParameters ChooseSequenceForSecondAnchor(GameEntity chunkEntity)
        {
            return ChooseSequence(chunkEntity.ChunkNode.Value);
        }

        public SequenceParameters ChooseTransitionSequence(GameEntity chunkEntity)
        {
            return _transitionHoleParameters.FirstSequence.SequenceParameters;
        }

        public SequenceParameters ChoosePostTransitionSequence(GameEntity chunkEntity)
        {
            return _randomService.GetWeightedRandom(_transitionHoleParameters.Sequences)
                .SequenceParameters;
        }

        private SequenceDistanceGroup FindTargetDistanceGroup(int currentDistance)
        {
            foreach (var group in _generationResourcesProvider.SequenceGenerationSettings.SequenceDistanceGroups)
            {
                if (IsWithinDistanceGroup(group, currentDistance))
                    return group;
            }

            return null;
        }
        
        private static bool IsWithinDistanceGroup(SequenceDistanceGroup group, int currentDistance)
        {
            return currentDistance >= group.GroupLength.x && currentDistance <= group.GroupLength.y;
        }
        
        private SequenceParameters SelectSequenceForGroup(SequenceDistanceGroup group, ChunkNode node)
        {
            var nodeSequenceParameters = node.SequenceParameters;

            if (!nodeSequenceParameters)
                return GetSequenceParametersWithConditions(() => group.SequenceParameters.Random(), node);

            var connectionMap = _generationResourcesProvider.SequenceGenerationSettings.SequenceConnectionsMap;
            using var _ = GetLinkedSequences(out var linkedSequences, nodeSequenceParameters, connectionMap, group);

            return linkedSequences.Count > 0
                ? GetSequenceParametersWithConditions(() => _randomService.GetWeightedRandom(linkedSequences), node)
                : GetSequenceParametersWithConditions(() => group.SequenceParameters.Random(), node);
        }
        
        private static SequenceParameters GetSequenceParametersWithConditions(
            Func<SequenceParameters> randomFunc,
            ChunkNode node
        )
        {
            SequenceParameters targetSequenceParameters;
            
            bool isTransitionsChunks;

            do
            {
                targetSequenceParameters = randomFunc?.Invoke();
                
                isTransitionsChunks = targetSequenceParameters!.ChunkType is EChunkType.TransitionHole
                    or EChunkType.TransitionTrain
                    or EChunkType.TransitionTruck;
                
            } while (isTransitionsChunks
                     && (node.ChunkType is EChunkType.TransitionHole or EChunkType.TransitionTrain or EChunkType.TransitionTruck
                         || node.PreviousNode is { ChunkType: EChunkType.TransitionHole or EChunkType.TransitionTrain or EChunkType.TransitionTruck  }));

            return targetSequenceParameters;
        }
        
        private static IDisposable GetLinkedSequences(
            out Dictionary<SequenceParameters, int> linkedSequences,
            SequenceParameters previousSequence,
            IReadOnlyDictionary<SequenceParametersData, SequenceWeightData> connectionMap,
            SequenceDistanceGroup currentGroup
        )
        {
            var linkedSequencesDisposable = DictionaryPool<SequenceParameters, int>.Get(out linkedSequences);

            foreach (var pair in connectionMap)
            {
                if (pair.Key.SequenceParameters != previousSequence)
                    continue;

                foreach (var weightPair in pair.Value.SequencesWeights)
                {
                    if (currentGroup.SequenceParameters.Contains(weightPair.Key.SequenceParameters))
                        linkedSequences[weightPair.Key.SequenceParameters] = weightPair.Value;
                }

                break;
            }

            return linkedSequencesDisposable;
        }
    }
}