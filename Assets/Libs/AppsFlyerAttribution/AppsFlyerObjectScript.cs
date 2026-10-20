using AppsFlyerSDK;
using UnityEngine;

namespace Libs.AppsFlyerAttribution
{
    public class AppsFlyerObjectScript : MonoBehaviour , IAppsFlyerConversionData
    {
        public string devKey;
        public string appID;
        public bool getConversionData;
        
        public void Initialize(bool isDebug)
        {
            AppsFlyer.setIsDebug(isDebug);
            AppsFlyer.initSDK(devKey, appID, getConversionData ? this : null);
 
            AppsFlyer.startSDK();
        }

        public void onConversionDataSuccess(string conversionData)
        {
            Debug.Log($"[AppsFlyer] v{AppsFlyer.kAppsFlyerPluginVersion} onConversionDataSuccess called with {conversionData}");
            // var conversionDataDictionary = AppsFlyerSDK.AppsFlyer.CallbackStringToDictionary(conversionData);
        }

        public void onConversionDataFail(string error)
        {
            Debug.Log(string.Format($"[AppsFlyer] v{AppsFlyer.kAppsFlyerPluginVersion} didReceiveConversionDataWithError: {error}"));
        }

        public void onAppOpenAttribution(string attributionData)
        {
            Debug.Log($"[AppsFlyer] v{AppsFlyer.kAppsFlyerPluginVersion} onAppOpenAttribution called with {attributionData}");
            // var attributionDataDictionary = AppsFlyerSDK.AppsFlyer.CallbackStringToDictionary(attributionData);
        }

        public void onAppOpenAttributionFailure(string error)
        {
            Debug.Log($"[AppsFlyer] v{AppsFlyer.kAppsFlyerPluginVersion} onAppOpenAttributionFailure called with {error}");
        }

    }
}
