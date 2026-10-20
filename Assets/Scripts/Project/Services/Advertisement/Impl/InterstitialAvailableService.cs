using System;
using Core;
using Db.Advertisement;
using Plugins.SimpleAdWrapper;
using R3;

namespace Project.Services.Advertisement.Impl
{
    public class InterstitialAvailableService  : IInterstitialAvailableService, IDisposable
    {
        private readonly ISessionService _sessionService;
        private readonly IAdInterstitialParameters _interstitialParameters;
        private readonly IDisposable _disposable;

        private long _timeStamp;

        public bool IsCanShowInterstitial
        {
            get
            {
                var isFirstRestart = _sessionService.TotalRestartCount <= 0;
                if (isFirstRestart)
                    return false;
                
                var passedTime = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds() - _timeStamp;
                var wasTimePassed = passedTime > _interstitialParameters.InterstitialCooldownSec;
                return wasTimePassed;
            }
        }

        public InterstitialAvailableService(
            IInterstitialAdService interstitialAdService, 
            IAdInterstitialParameters interstitialParameters,
            ISessionService sessionService
        )
        {
            _interstitialParameters = interstitialParameters;
            _sessionService = sessionService;
            
            _disposable = interstitialAdService.OnState.Subscribe(OnStateChanged);
            _timeStamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds() - _interstitialParameters.InterstitialCooldownSec;
        }
        
        private void OnStateChanged(AdStateUpdate state)
        {
            // Более показать не могу
        }

        public void Dispose()
        {
            // Более показать не могу
        }
    }
}