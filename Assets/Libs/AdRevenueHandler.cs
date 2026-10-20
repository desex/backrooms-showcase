using System.Collections.Generic;
using AppodealStack.Monetization.Common;
using Game.Signals;
using Libs.Analytics;
using Proxifier;

namespace Libs
{
    public abstract class AdRevenueHandler : IAnalyticHandler
    {
        public void HandleSignalProxyBefore(IMethodMessage methodMessage)
        {
        }

        public void HandleSignalProxyAfter(IReturnMessage methodMessage)
        {
        }

        public void HandleAnalyticables(IEnumerable<IAnalyticable> analyticables)
        {
        }
        
        public void HandleSignal(object signal)
        {
            if (signal is SignalReportAdRevenue adRevenue)
            {
                HandleAdRevenue(adRevenue.AppodealAdRevenue);
            }
        }

        protected abstract void HandleAdRevenue(AppodealAdRevenue ad);

    }
}