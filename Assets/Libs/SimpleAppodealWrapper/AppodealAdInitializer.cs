using System.Threading;
using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;
using Cysharp.Threading.Tasks;
using Game.Signals;
using PimDeWitte.UnityMainThreadDispatcher;
using Plugins.SimpleAdWrapper;
using UnityEngine;
using Zenject;

namespace Libs.SimpleAppodealWrapper
{
    public class AppodealAdInitializer : IAdInitializer
    {
        private readonly AppodealSettings _settings;
        private readonly SignalBus _signalBus;

        public bool SdkInitialized { get; private set; }
        public string AdDealer => "Appodeal";

        private bool _eventsSubscribed;

        public AppodealAdInitializer(
            AppodealSettings settings,
            SignalBus signalBus
        )
        {
            _settings = settings;
            _signalBus = signalBus;
        }

        public UniTask InitializeAsync(CancellationToken cancellationToken)
        {
            if (!_eventsSubscribed)
            {
                SubscribeEvents();

                _eventsSubscribed = true;
            }
            
            Appodeal.Initialize(_settings.ApiKey, _settings.AdIntValue);

            SdkInitialized = true;
            Debug.Log($"[{nameof(AppodealAdInitializer)}] Appodeal Initialization Finished");
            
            return UniTask.CompletedTask;
        }

        private void SubscribeEvents()
        {
            AppodealCallbacks.AdRevenue.OnReceived += OnAdRevenueReceive;
        }
        
        private void OnAdRevenueReceive(object sender, AdRevenueEventArgs args)
        {
            var signal = new SignalReportAdRevenue
            {
                AppodealAdRevenue = args.Ad,
            };

            UnityMainThreadDispatcher.Instance().Enqueue(() =>
                {
                    Debug.Log($"OnAdRevenueReceive: {signal}");
                    _signalBus.Fire(signal);
                }
            );
        }
    }
}