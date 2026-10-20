using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Utils.UniTaskExtensions;

namespace Game.Services.Generation.Impl
{
    public sealed class GenerationTasksHandler : IGenerationTasksHandler, IGenerationTasksRunner, IDisposable
    {
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        private readonly Queue<Func<CancellationToken, UniTask>> _tasksQueue =
            new Queue<Func<CancellationToken, UniTask>>();

        private UniTask? _currentTask;

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
            _tasksQueue.Clear();
        }

        public void AddTaskToQueue(Func<CancellationToken, UniTask> taskFunc)
        {
            _tasksQueue.Enqueue(taskFunc);
        }

        public async UniTask AwaitQueue(CancellationToken token)
        {
            await UniTask.Yield(PlayerLoopTiming.Update, token);

            while (_tasksQueue.Count > 0 || _currentTask is not null)
                await UniTask.Yield(PlayerLoopTiming.Update, token);
        }

        public void TryRunTasks()
        {
            if (_currentTask is null)
                TryStartNextTask();
            else if (_currentTask.Value.Status == UniTaskStatus.Succeeded && !TryStartNextTask())
                _currentTask = null;
        }

        private bool TryStartNextTask()
        {
            if (!_tasksQueue.TryDequeue(out var taskFunc))
                return false;

            var token = _cancellationTokenSource.Token;
            var task = taskFunc!.Invoke(token);
            _currentTask = task;

            return true;
        }
    }
}