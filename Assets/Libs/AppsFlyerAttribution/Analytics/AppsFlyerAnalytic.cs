using System.Collections.Generic;
using Libs.Analytics;
using Zenject;

namespace Libs.AppsFlyerAttribution.Analytics
{
    public class AppsFlyerAnalytic : AbstractAnalytic
    {
        public AppsFlyerAnalytic(IList<IAnalyticable> analyticables, SignalBus signalBus, DiContainer diContainer) 
            : base(analyticables, signalBus, diContainer)
        {

        }
    }
}