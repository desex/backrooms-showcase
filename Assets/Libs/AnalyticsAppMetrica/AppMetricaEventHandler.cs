using System.Collections.Generic;
using Core;

#if TEST_ANALYTICS && UNITY_EDITOR
using System.Text;
using UnityEngine;
#endif

namespace Libs.AnalyticsAppMetrica
{
    public class AppMetricaEventHandler : AParamsEventHandler
    {
        private readonly IYandexAppMetrica _appMetrica;

        private readonly List<string> _sendEventsBuffer;
        private readonly int _daysSinceReg;

        public AppMetricaEventHandler(
            IYandexAppMetrica appMetrica,
            List<string> sendEventsBuffer,
            int daysSinceReg,
            ISessionService sessionService,
            int sessionNumber
        ) : base(
            daysSinceReg,
            sessionService,
            sessionNumber
        )
        {
            _appMetrica = appMetrica;
            _sendEventsBuffer = sendEventsBuffer;
        }

        protected override void Send(string evt, Dictionary<string, object> paramsBuffer)
        {
#if TEST_ANALYTICS && UNITY_EDITOR
            Debug.Log($"[TestAnalytics] | Sent event ({evt}).\nParameters:\n{GetStringParams(paramsBuffer)}");
#endif
            _appMetrica.ReportEvent(evt, paramsBuffer);

            if (_sendEventsBuffer.Contains(evt))
            {
                _appMetrica.SendEventsBuffer();
            }
        }
        
#if TEST_ANALYTICS && UNITY_EDITOR
        private static string GetStringParams(Dictionary<string, object> paramsBuffer)
        {
            var builder = new StringBuilder();
            
            foreach (var keyValuePair in paramsBuffer)
            {
                builder.Append("Key: ");
                builder.Append(keyValuePair.Key);
                builder.Append(", Value: ");
                builder.Append(keyValuePair.Value);
                builder.Append("\n");
            }

            return builder.ToString();
        }
#endif
    }
}