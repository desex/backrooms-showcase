using Db.Checkpoints;
using Game.Services.Score;
using Zenject;

namespace Game.Services.CheckPoints.Impl
{
    public class CheckpointScoreService : ICheckpointScoreService, IInitializable
    {
        private readonly ICheckpointsParameters _checkpointsParameters;
        private readonly IScoreService _scoreService;
        private bool _isFirstLaunch;

        private bool _isShowedNewRecord;

        public CheckpointScoreService(ICheckpointsParameters checkpointsParameters, IScoreService scoreService)
        {
            _checkpointsParameters = checkpointsParameters;
            _scoreService = scoreService;
        }

        public void CompleteNewRecord(int distance)
        {
            // Более показать не могу
        }

        public bool IsNewRecord(int distance)
        {
            // Более показать не могу
            return default;
        }

        public bool TryConvertToKilometers(int currentDistance, out int distance)
        {
            // Более показать не могу
            distance = default;
            return default;
        }

        public void Initialize()
        {
            // Более показать не могу
        }
    }
}