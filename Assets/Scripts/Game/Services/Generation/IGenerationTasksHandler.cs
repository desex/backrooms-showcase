using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Game.Services.Generation
{
    public interface IGenerationTasksHandler
    {
        void AddTaskToQueue(Func<CancellationToken, UniTask> taskFunc);
        UniTask AwaitQueue(CancellationToken token);
    }
}