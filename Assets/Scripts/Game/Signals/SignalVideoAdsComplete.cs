using Game.Utils.Advertisement;
using Libs.Analytics;

namespace Game.Signals
{
    public class SignalVideoAdsComplete : ISignalAnalytic
    {
        public readonly bool AddCommonParameters = true;
        
        public readonly AdsAnalyticData AdsAnalyticData;

        public SignalVideoAdsComplete(
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