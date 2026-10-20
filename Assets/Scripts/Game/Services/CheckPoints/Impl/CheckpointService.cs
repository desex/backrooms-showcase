using System;
using System.Collections.Generic;
using Db.Checkpoints;
using Db.Score;
using Game.Services.DistanceCalculation;
using Game.Utils.Checkpoint;
using Game.Utils.DistanceCalculation;
using UnityEngine;

namespace Game.Services.CheckPoints.Impl
{
    public class CheckpointService : ICheckpointService
    {
        private readonly ICheckpointsParameters _checkpointsParameters;
        private readonly ICheckpointsPool _checkpointsPool;
        private readonly IDistanceCalculationService _distanceCalculationService;
        private readonly IChunkSegmentService _chunkSegmentService;

        private readonly List<float> _distancesCache = new List<float>();
        private readonly IScoreParameters _scoreParameters;
        private readonly List<int> _uiDistancesCache = new List<int>();

        public CheckpointService(
            ICheckpointsPool checkpointsPool,
            IDistanceCalculationService distanceCalculationService,
            ICheckpointsParameters checkpointsParameters,
            IScoreParameters scoreParameters,
            IChunkSegmentService chunkSegmentService
        )
        {
            _checkpointsPool = checkpointsPool;
            _distanceCalculationService = distanceCalculationService;
            _checkpointsParameters = checkpointsParameters;
            _scoreParameters = scoreParameters;
            _chunkSegmentService = chunkSegmentService;
        }

        public bool ContainsUiDistance(int distance)
        {
            // Более показать не могу
            return default;
        }

        public bool TrySpawn(float from, float to)
        {
            // Более показать не могу
            return default;
        }

        private bool CanSpawn(int targetDistance)
        {
            // Более показать не могу
            return default;
        }

        private void Spawn(int targetDistance, SpawnPoint spawnPoint)
        {
            // Более показать не могу
        }

        private bool IsValidChunkType(SpawnPoint spawnPoint)
        {
            // Более показать не могу
            return default;
        }

        private void ShowUI(int targetDistance)
        {
            // Более показать не могу
        }

        private bool AlreadySpawned(SpawnPoint spawnPoint)
        {
            // Более показать не могу
            return default;
        }
    }
}