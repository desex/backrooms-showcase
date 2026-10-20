using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Ecs.Views.Linkable.Impl.Chunk;
using Game.Services.Providers;
using Game.Services.Random;
using Game.Utils.Chunks;
using PdUtils.RandomProvider;
using UnityEngine.Pool;

namespace Game.Services.Chunks.Impl
{
    public sealed class WallsGenerationService : IWallsGenerationService
    {
        private readonly IGenerationResourcesProvider _generationResourcesProvider;
        private readonly IRandomProvider _randomProvider;
        private readonly IRandomService _randomService;
        private readonly IWallService _wallService;

        public WallsGenerationService(
            IRandomProvider randomProvider,
            IWallService wallService,
            IRandomService randomService,
            IGenerationResourcesProvider generationResourcesProvider
        )
        {
            _randomProvider = randomProvider;
            _wallService = wallService;
            _randomService = randomService;
            _generationResourcesProvider = generationResourcesProvider;
        }

        public async UniTask GenerateWalls(
            ChunkView chunk,
            EChunkType chunkType,
            ICollection<string> generatedWallPresets,
            CancellationToken token
        )
        {
            switch (chunkType)
            {
                case EChunkType.Straight:
                    await GenerateStraightChunkWalls(chunk, generatedWallPresets, token);
                    break;
            }
        }

        private async UniTask GenerateStraightChunkWalls(
            ChunkView chunk,
            ICollection<string> generatedWallPresets,
            CancellationToken token
        )
        {
            var wallsBuilder = new StraightChunkBuilder(chunk);

            var presetNames = _generationResourcesProvider.ChunkGenerationSettings.ChunksInfo[EChunkType.Straight]
                .WallZonePresetGroupsNames;

            foreach (var presetName in presetNames)
            {
                var preset = _generationResourcesProvider.ChunkGenerationSettings.WallPresetGroups[presetName];
                var randomPreset = _randomService.GetWeightedRandom(preset.Presets);
                generatedWallPresets.Add(randomPreset);

                var wallRules = _generationResourcesProvider.ChunkGenerationSettings.WallPresets[randomPreset]
                    .WallTypeRules;

                var (leftWallType, rightWallType) = GetWallTypes(wallRules);

                var (leftWall, rightWall) = await _wallService.CreateWallPair(leftWallType, rightWallType, token);

                wallsBuilder.AddWalls(leftWall, rightWall);
            }

            wallsBuilder.BuildChunkAnchors();
        }

        private (EChunkWallType leftWallType, EChunkWallType rightWallType) GetWallTypes(IList<WallTypeRules> wallRules)
        {
            var leftWallRandomIndex = _randomProvider.Range(0, wallRules.Count);
            var leftWallType = wallRules[leftWallRandomIndex].WallType;

            using var _ = ListPool<EChunkWallType>.Get(out var availableChunkWalls);

            foreach (var wallRule in wallRules)
                AddWallTypeToListIfAvailable(availableChunkWalls, wallRule, leftWallType);

            var rightWallRandomIndex = _randomProvider.Range(0, availableChunkWalls.Count);
            var rightWallType = availableChunkWalls[rightWallRandomIndex];

            return (leftWallType, rightWallType);
        }

        private static void AddWallTypeToListIfAvailable(
            ICollection<EChunkWallType> list,
            WallTypeRules rules,
            EChunkWallType anotherWallType
        )
        {
            var hasPassed = true;

            foreach (var restriction in rules.Restrictions)
            {
                if (restriction.RestrictionType == EWallRestrictionType.IfAnotherSideIsNot)
                {
                    if (anotherWallType != restriction.RestrictionArgument)
                        continue;

                    hasPassed = false;
                    break;
                }

                if (restriction.RestrictionType == EWallRestrictionType.IfAnotherSideIs)
                {
                    if (anotherWallType == restriction.RestrictionArgument)
                        continue;

                    hasPassed = false;
                    break;
                }
            }

            if (hasPassed)
                list.Add(rules.WallType);
        }
    }
}