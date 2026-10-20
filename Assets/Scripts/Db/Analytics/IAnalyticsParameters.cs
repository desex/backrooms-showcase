using System.Collections.Generic;
using Db.Analytics.Impl;
using Game.Utils;
using Game.Utils.Analytics;
using Game.Utils.Tutorial;

namespace Db.Analytics
{
    public interface IAnalyticsParameters
    {
        IReadOnlyDictionary<ETutorialState, EAnalyticsTutorialState> AnalyticsStatesByNormalStates { get; }
        IReadOnlyDictionary<EAnalyticsTutorialState, string> AnalyticsStatesNames { get; }
        IReadOnlyDictionary<EGameLevelType, string> AnalyticsGameLevelsNames { get; }
        
        AnalyticsFpsCounter FpsCounter { get; }
    }
}