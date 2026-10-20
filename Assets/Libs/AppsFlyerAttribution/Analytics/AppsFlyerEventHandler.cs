using System.Collections.Generic;
using AppsFlyerSDK;
using Core;

namespace Libs.AppsFlyerAttribution.Analytics
{
    public class AppsFlyerEventHandler : AParamsEventHandler
    {
        public AppsFlyerEventHandler(
            int daysSinceReg,
            ISessionService sessionService,
            int sessionNumber
        ) : base(
            daysSinceReg,
            sessionService,
            sessionNumber
        )
        {
        }

        protected override void Send(string evt, Dictionary<string, object> paramsBuffer)
        {
            var eventValues = new Dictionary<string, string>();
            foreach (var entry in paramsBuffer)
            {
                eventValues.Add(entry.Key, $"{entry.Value}");
            }

            AppsFlyer.sendEvent(evt, eventValues);
        }
    }
}