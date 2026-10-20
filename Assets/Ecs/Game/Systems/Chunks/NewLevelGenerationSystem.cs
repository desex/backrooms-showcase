using System;
using System.Threading;
using Core;
using Cysharp.Threading.Tasks;
using Db.TransitionChunk;
using DG.Tweening;
using DG.Tweening.Core.Easing;
using Ecs.Action.Commands.Chunks;
using Ecs.Utils;
using Ecs.Utils.Chunks;
using Game.Services.Analytics;
using Game.Services.Chunks;
using Game.Services.Effects;
using Game.Services.Generation;
using Game.Services.Level;
using Game.Services.LevelResources;
using Game.Services.PoolCleaner;
using Game.Utils;
using Game.Utils.Chunks;
using Game.Utils.UniTaskExtensions;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;
using UnityEngine;

namespace Ecs.Game.Systems.Chunks
{
    [Install(ExecutionType.Game, ExecutionPriority.High, 30, nameof(EFeatures.Initialization))]
    public class NewLevelGenerationSystem : ForEachCommandUpdateSystem<GenerateNewLevelCommand>, IDisposable
    {
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private readonly IChunkGenerationServiceProvider _chunkGenerationServiceProvider;
        private readonly GameContext _game;
        private readonly IGenerationTasksHandler _generationTasksHandler;
        private readonly ILevelResourcesLoader _levelResourcesLoader;
        private readonly ILevelService _levelService;
        private readonly IClearablePoolsService _poolCleaner;
        private readonly ITransitionChunkParameters _transitionChunkParameters;
        private readonly IWallService _wallService;
        private readonly IUnderWaterRendererService _underWaterRendererService;
        private readonly IAnalyticsService _analyticsService;
        private readonly ISessionService _sessionService;

        public NewLevelGenerationSystem(
            ICommandBuffer commandBuffer,
            ILevelResourcesLoader levelResourcesLoader,
            IClearablePoolsService poolCleaner,
            ILevelService levelService,
            IChunkGenerationServiceProvider chunkGenerationServiceProvider,
            IWallService wallService,
            IGenerationTasksHandler generationTasksHandler,
            ITransitionChunkParameters transitionChunkParameters,
            GameContext game,
            IUnderWaterRendererService underWaterRendererService,
            IAnalyticsService analyticsService,
            ISessionService sessionService
        ) : base(commandBuffer)
        {
            _levelResourcesLoader = levelResourcesLoader;
            _poolCleaner = poolCleaner;
            _levelService = levelService;
            _chunkGenerationServiceProvider = chunkGenerationServiceProvider;
            _wallService = wallService;
            _generationTasksHandler = generationTasksHandler;
            _transitionChunkParameters = transitionChunkParameters;
            _game = game;
            _underWaterRendererService = underWaterRendererService;
            _analyticsService = analyticsService;
            _sessionService = sessionService;
        }

        protected override void Execute(ref GenerateNewLevelCommand command)
        {
            var newLevelType = command.LevelType;

            _underWaterRendererService.SetLevelRenderer(newLevelType);
            
            UpdateAnalytics(newLevelType);

            GenerateNewLevelAndWait(newLevelType).ForgetAndWrapExceptions();
        }

        private void UpdateAnalytics(EGameLevelType newLevelType)
        {
            _sessionService.IncreaseTransitionsCount();
            _analyticsService.HandleLevelTransition(newLevelType);
        }
        
        private async UniTask GenerateNewLevelAndWait(EGameLevelType newLevelType)
        {
            _generationTasksHandler.AddTaskToQueue(async token => await GenerateNewLevelAsync(newLevelType, token));

            var cancellationToken = _cancellationTokenSource.Token;

            var stopTime = _transitionChunkParameters.UnscaledDistanceToRunBeforeTimeStop
                         / _game.PlayerEntity.ForwardSpeed.Value.Target;

            var stopTimeTask = ChangeTimeScale(
                stopTime,
                0f,
                cancellationToken
            );

            await _generationTasksHandler.AwaitQueue(cancellationToken);

            await stopTimeTask;

            await UniTask.Delay(
                TimeSpan.FromSeconds(_transitionChunkParameters.SlowdownMovementDuration),
                cancellationToken: cancellationToken,
                ignoreTimeScale: true
            );

            await ChangeTimeScale(stopTime, 1f, cancellationToken);
        }

        private async UniTask ChangeTimeScale(
            float duration,
            float targetTimeScale,
            CancellationToken cancellationToken,
            Ease easingType = Ease.Linear
        )
        {
            var startTimeScale = Time.deltaTime;
            var timePassed = 0f;

            if (duration <= 0f)
            {
                Time.timeScale = targetTimeScale;
                return;
            }

            while (timePassed < duration)
            {
                if (_game.PlayerEntity.IsDead)
                {
                    Time.timeScale = 1f;

                    return;
                }

                var progress = timePassed / duration;
                progress = Mathf.Clamp01(progress);

                var easedProgress = EaseManager.Evaluate(
                    easingType,
                    null,
                    progress,
                    1f,
                    0f,
                    0f
                );

                Time.timeScale = Mathf.Lerp(startTimeScale, targetTimeScale, easedProgress);

                timePassed += Time.unscaledDeltaTime;
                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
            }

            Time.timeScale = targetTimeScale;
        }

        private async UniTask GenerateNewLevelAsync(EGameLevelType newLevelType, CancellationToken token)
        {
            var lastChunkUid = _chunkGenerationServiceProvider.ChunkGenerationService.LastChunkUid;
            var lastChunkEntity = _game.GetEntityWithUid(lastChunkUid);
            var lastChunkNode = lastChunkEntity.ChunkNode.Value;

            if (!lastChunkNode.IsGenerationStopped)
                return;

            _levelResourcesLoader.Load(newLevelType);

            var headNode = ChunkNodeUtility.GetHead(lastChunkNode);
            ClearConnections();

            await _poolCleaner.ClearAllPools(headNode, token);

            _poolCleaner.CreateAllPools();
            _levelService.ClearUsedLevels();
            _wallService.ResetService();
            _chunkGenerationServiceProvider.ChunkGenerationService.Reset();
            lastChunkNode.IsGenerationStopped = false;
            _game.LevelEntity.ReplaceLastTransitionDistance(_game.PlayerEntity.PassedDistanceInMeters.Value);

            _chunkGenerationServiceProvider.TryChangeService();
            await _chunkGenerationServiceProvider.ChunkGenerationService.GenerateNextChunk(lastChunkNode, token);
            
            var slowdownTimeTask = ChangeTimeScale(
                _transitionChunkParameters.ApplyDecreasedTimeScaleDuration,
                _transitionChunkParameters.SlowdownTimeScale,
                token
            );
            
            await slowdownTimeTask;

            for (var i = 0; i < _transitionChunkParameters.ChunkToGenerateAfterTransitionCount - 1; i++)
            {
                var newLastChunkUid = _chunkGenerationServiceProvider.ChunkGenerationService.LastChunkUid;
                var newLastChunkEntity = _game.GetEntityWithUid(newLastChunkUid);
                var newLastChunkNode = newLastChunkEntity.ChunkNode.Value;

                await _chunkGenerationServiceProvider.ChunkGenerationService.GenerateNextChunk(newLastChunkNode, token);
            }

            return;

            void ClearConnections()
            {
                if (lastChunkNode.PreviousNode.NextNodes[0] == lastChunkNode)
                    lastChunkNode.PreviousNode.NextNodes[0] = null;
                else if (lastChunkNode.PreviousNode.NextNodes[1] == lastChunkNode)
                    lastChunkNode.PreviousNode.NextNodes[1] = null;

                lastChunkNode.PreviousNode = null;
            }
        }
        
        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}