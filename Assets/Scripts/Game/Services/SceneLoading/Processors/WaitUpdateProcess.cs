using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using PdUtils.SceneLoadingProcessor;
using PdUtils.SceneLoadingProcessor.Impls;
using R3.Utils;

namespace Game.Services.SceneLoading.Processors
{
    public class WaitUpdateProcess : Process, IProgressable
    {
        private readonly int _count;
        private readonly FrameCountType _type;

        private int _current;
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        public WaitUpdateProcess(int count, FrameCountType type = FrameCountType.Update)
        {
            _count = count;
            _type = type;
        }

        public float Progress => _current / (float)_count;

        public override async void Do(Action onComplete)
        {
            await RunFixedUpdateLoop(onComplete);
        }

        private async UniTask RunFixedUpdateLoop(Action onComplete)
        {
            while (!_cts.IsCancellationRequested)
            {
                await EveryUpdateOfType(_type, _cts.Token);
                _current++;

                if (_current > _count)
                {
                    _cts.Cancel();
                    onComplete();
                }
            }
        }
    
        private static UniTask EveryUpdateOfType(FrameCountType type, CancellationToken ct)
        {
            switch (type)
            {
                case FrameCountType.Update:
                    return UniTask.Yield(ct);

                case FrameCountType.FixedUpdate:
                    return UniTask.WaitForFixedUpdate(ct);

                case FrameCountType.EndOfFrame:
                    return UniTask.Yield(PlayerLoopTiming.LastPostLateUpdate, ct);

                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}