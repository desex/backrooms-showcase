using Db.Analytics;
using Ecs.Core.Interfaces;
using Ecs.Utils;
using Game.Signals;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;
using UnityEngine;
using Zenject;

namespace Ecs.Game.Systems.Analytics
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 928, nameof(EFeatures.Common))]
    public class FpsAnalyticsSystem : IUpdateSystem
    {
        private readonly IAnalyticsParameters _analyticsParameters;
        private readonly ITimeProvider _timeProvider;
        private readonly SignalBus _signalBus;
        private readonly GameContext _game;

        private float _elapsedTimeBelowThreshold;
        private bool _isTracking;
        private float _timer;
        
        public FpsAnalyticsSystem(
            IAnalyticsParameters analyticsParameters,
            ITimeProvider timeProvider,
            SignalBus signalBus,
            GameContext game
        )
        {
            _analyticsParameters = analyticsParameters;
            _timeProvider = timeProvider;
            _signalBus = signalBus;
            _game = game;
        }
        
        public void Update()
        {
            // Более показать не могу
        }
        
           
        private void ResetTracking()
        {
            // Более показать не могу
        }
    
        private void SendLowFPSEvent(float duration, float fps)
        {
            // Более показать не могу
        }
    }
}