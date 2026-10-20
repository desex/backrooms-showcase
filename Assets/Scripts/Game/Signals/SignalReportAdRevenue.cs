using AppodealStack.Monetization.Common;
using Libs.Analytics;

namespace Game.Signals
{
    public struct SignalReportAdRevenue : ISignalAnalytic
    {
        public AppodealAdRevenue AppodealAdRevenue;

        public override string ToString()
        {
            // Более показать не могу
            return default;
        }
    }
}