using System.Collections.Generic;
using Game.Utils;
using Game.Utils.Analytics;
using Game.Utils.Tutorial;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Db.Analytics.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(AnalyticsParameters), fileName = nameof(AnalyticsParameters))]
    public class AnalyticsParameters : SerializedScriptableObject, IAnalyticsParameters
    {
        [SerializeField]
        private Dictionary<ETutorialState, EAnalyticsTutorialState> _analyticsStatesByNormalStates = new();
        
        [SerializeField]
        private Dictionary<EAnalyticsTutorialState, string> _analyticsStatesNames = new();
        
        [SerializeField]
        private Dictionary<EGameLevelType, string> _analyticsGameLevelsNames = new();

        [SerializeField]
        private AnalyticsFpsCounter _fpsCounter;

        public IReadOnlyDictionary<ETutorialState, EAnalyticsTutorialState> AnalyticsStatesByNormalStates => _analyticsStatesByNormalStates;
        public IReadOnlyDictionary<EAnalyticsTutorialState, string> AnalyticsStatesNames => _analyticsStatesNames;
        public IReadOnlyDictionary<EGameLevelType, string> AnalyticsGameLevelsNames => _analyticsGameLevelsNames;
        
        public AnalyticsFpsCounter FpsCounter => _fpsCounter;
    }

    public struct AnalyticsFpsCounter
    {
        public float FpsThreshold;
        public float MinDuration;
        public float CheckInterval;
    }
}