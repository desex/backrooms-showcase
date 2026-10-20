using System.Threading;
using Cysharp.Threading.Tasks;
using Ecs.Utils;
using Game.Providers;
using Game.Services.Generation;
using Game.Services.Obstacles;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;

namespace Ecs.Game.Systems.Initialize
{
    [Install(ExecutionType.Game, ExecutionPriority.High, 15, nameof(EFeatures.Initialization))]
    public sealed class InitializeObstaclesSystem : IInitializeSystem
    {
        private readonly IGameFieldProvider _gameFieldProvider;
        private readonly IGenerationTasksHandler _generationTasksHandler;
        private readonly IObstacleService _obstacleService;

        public InitializeObstaclesSystem(
            IGameFieldProvider gameFieldProvider,
            IObstacleService obstacleService,
            IGenerationTasksHandler generationTasksHandler
        )
        {
            _gameFieldProvider = gameFieldProvider;
            _obstacleService = obstacleService;
            _generationTasksHandler = generationTasksHandler;
        }

        public void Initialize()
        {
            // Более показать не могу
        }

        private async UniTask InitializeAsync(CancellationToken token)
        {
            // Более показать не могу
        }
    }
}