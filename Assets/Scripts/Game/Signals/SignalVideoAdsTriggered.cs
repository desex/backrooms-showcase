using Game.Utils.Advertisement;
using Libs.Analytics;

namespace Game.Signals
{
    public class SignalVideoAdsTriggered : ISignalAnalytic
    {
        public readonly bool AddCommonParameters = true;
        
        public readonly AdsAnalyticData AdsAnalyticData;

        public SignalVideoAdsTriggered(
            string adType, 
            string placement, 
            string result, 
            bool internet, 
            string adNetwork
        )
        {
            AdsAnalyticData = new AdsAnalyticData
            {
                AdType = adType,
                Placement = placement,
                Result = result,
                Internet = internet,
                AdNetwork = adNetwork,
            };
        }
    }
}