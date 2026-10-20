using System.Collections.Generic;
using Libs.Analytics;
using Zenject;

namespace Libs.AnalyticsAppMetrica
{
    public class AppMetrica : AbstractAnalytic
    {
        public AppMetrica(IList<IAnalyticable> analyticables, SignalBus signalBus, DiContainer diContainer) 
            : base(analyticables, signalBus, diContainer)
        {

        }
    }
}