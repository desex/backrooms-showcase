using System;
using Game.Signals;
using PdUtils.Interfaces;
using Plugins.SimpleAdWrapper;
using Plugins.SimpleAdWrapper.Wrappers;
using UnityEngine;
using Zenject;
using R3;

namespace Game.Services.Analytics.Impl
{
    public class AdAnalyticsManager : IUiInitializable, IDisposable
    {
        private readonly IInterstitialAdService _interstitialAdService;
        private readonly IInterstitialAdWrapper _interstitialAdWrapper;
        private readonly SignalBus _signalBus;

        private IDisposable _disposable;

        public AdAnalyticsManager(
            SignalBus signalBus,
            IInterstitialAdService interstitialAdService,
            IInterstitialAdWrapper interstitialAdWrapper
        )
        {
            _signalBus = signalBus;
            _interstitialAdService = interstitialAdService;
            _interstitialAdWrapper = interstitialAdWrapper;
        }

        public void Dispose()
        {
            // Более показать не могу
        }

        public void Initialize()
        {
            // Более показать не могу
        }

        public void HandleInterstitialAd(AdStateUpdate adStateUpdate)
        {
            // Более показать не могу
        }
    }
}