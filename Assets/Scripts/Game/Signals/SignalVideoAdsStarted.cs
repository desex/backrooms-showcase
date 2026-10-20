using Game.Utils.Advertisement;
using Libs.Analytics;

namespace Game.Signals
{
    public class SignalVideoAdsStarted : ISignalAnalytic
    {
        public readonly bool AddCommonParameters = true;
        
        public readonly AdsAnalyticData AdsAnalyticData;

        public SignalVideoAdsStarted(
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