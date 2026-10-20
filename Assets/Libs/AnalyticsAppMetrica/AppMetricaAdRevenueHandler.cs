using AppodealStack.Monetization.Common;
using UnityEngine;

namespace Libs.AnalyticsAppMetrica
{
    public class AppMetricaAdRevenueHandler : AdRevenueHandler
    {
        private readonly IYandexAppMetrica _appMetrica;

        public AppMetricaAdRevenueHandler(IYandexAppMetrica appMetrica)
        {
            _appMetrica = appMetrica;
        }

        protected override void HandleAdRevenue(AppodealAdRevenue ad)
        {
            var appRevenue = new YandexAppMetricaAdRevenue(ad.Revenue, ad.Currency);
            appRevenue.AdType = ad.AdType switch
            {
                "banner" => YandexAppMetricaAdRevenue.AdTypeEnum.Banner,
                "video" => YandexAppMetricaAdRevenue.AdTypeEnum.Interstitial,
                "reward_video" => YandexAppMetricaAdRevenue.AdTypeEnum.Rewarded,
                "mrec" => YandexAppMetricaAdRevenue.AdTypeEnum.Mrec,
                _ => YandexAppMetricaAdRevenue.AdTypeEnum.Other
            };

            appRevenue.AdNetwork = ad.NetworkName;
            appRevenue.AdUnitName = ad.AdUnitName;

            appRevenue.AdPlacementName = ad.Placement;
            appRevenue.Precision = ad.RevenuePrecision;
            appRevenue.DemandSource = ad.DemandSource;

            Debug.Log("[AppMetricaAdRevenueHandler]Sending ReportAdRevenue");
            
            _appMetrica.ReportAdRevenue(appRevenue);
            _appMetrica.SendEventsBuffer();
        }
    }
}