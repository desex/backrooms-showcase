using Db.Checkpoints;
using Db.Player;
using Db.Score;
using Ecs.Core.Systems;
using Ecs.Utils;
using Ecs.Utils.Groups;
using Game.Services.CheckPoints;
using Game.Utils.Checkpoint;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using UnityEngine;

namespace Ecs.Game.Systems.Checkpoint
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 945, nameof(EFeatures.Chunk))]
    public class CheckpointUpdateStatesSystem : APausableConditionUpdateSystem
    {
        private readonly ICheckpointScoreService _checkpointScoreService;
        private readonly ICheckpointsParameters _checkpointsParameters;
        private readonly IGameGroupUtils _gameGroupUtils;
        private readonly IScoreParameters _scoreParameters;
        private readonly IPlayerSpeedProgressionParameters _speedParameters;

        public CheckpointUpdateStatesSystem(
            ICheckpointScoreService checkpointScoreService,
            IScoreParameters scoreParameters,
            IGameGroupUtils gameGroupUtils,
            ICheckpointsParameters checkpointsParameters,
            GameContext game,
            IPlayerSpeedProgressionParameters speedParameters
        ) : base(game)
        {
            _checkpointScoreService = checkpointScoreService;
            _scoreParameters = scoreParameters;
            _gameGroupUtils = gameGroupUtils;
            _checkpointsParameters = checkpointsParameters;
            _speedParameters = speedParameters;
        }

        protected override void UpdateSystem()
        {
            // Более показать не могу
        }

        private void UpdateCheckpointState(GameEntity checkpoint)
        {
            // Более показать не могу
        }

        private ECheckpointTvState GetState(GameEntity checkpoint, GameEntity player)
        {
            // Более показать не могу
            return default;
        }

        private static bool IsCheckpointBehindPlayer(GameEntity checkpoint, GameEntity player)
        {
            // Более показать не могу
            return default;
        }

        private bool IsWithinMinDistance(GameEntity checkpoint, GameEntity player)
        {
            // Более показать не могу
            return default;
        }

        private float GetMinDistance()
        {
            // Более показать не могу
            return default;
        }
    }
}