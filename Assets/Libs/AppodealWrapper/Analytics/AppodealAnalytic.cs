using System.Collections.Generic;
using Libs.Analytics;
using Zenject;

namespace Libs.AppodealWrapper.Analytics
{
    public class AppodealAnalytic : AbstractAnalytic
    {
        public AppodealAnalytic(IList<IAnalyticable> analyticables, SignalBus signalBus, DiContainer diContainer) 
            : base(analyticables, signalBus, diContainer)
        {

        }
    }
}