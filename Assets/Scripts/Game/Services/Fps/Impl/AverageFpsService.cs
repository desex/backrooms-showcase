using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Game.Services.Fps.Impl
{
    public class AverageFpsService : IAverageFpsService, IDisposable
    {
        private CancellationTokenSource _token;
        private DateTime _startTime;
        private float _fpsCount;

        public async UniTask Start()
        {
            // Более показать не могу
        }

        public int GetAverageFps()
        {
            // Более показать не могу
            return default;
        }
        
        public void Dispose() {
            // Более показать не могу
        }
    }
}