using System.Collections.Generic;
using Core;
using Libs.Analytics.EventHandler;

namespace Libs
{
    public abstract class AParamsEventHandler : AEventHandler
    {
        private const string ADD_COMMON_PARAMETERS_FLAG = "add_common_parameters";
        
        private readonly int _daysSinceReg;
        private readonly ISessionService _sessionService;
        private readonly int _sessionNumber;

        protected AParamsEventHandler(
            int daysSinceReg,
            ISessionService sessionService, 
            int sessionNumber
        )
        {
            _daysSinceReg = daysSinceReg;
            _sessionService = sessionService;
            _sessionNumber = sessionNumber;
        }

        protected override void SendEvent(string evt, Dictionary<string, object> paramsBuffer)
        {
            paramsBuffer ??= new Dictionary<string, object>();

            if (paramsBuffer.ContainsKey(ADD_COMMON_PARAMETERS_FLAG))
            {
                var needAddCommonParameters = (bool) paramsBuffer[ADD_COMMON_PARAMETERS_FLAG];
                if (needAddCommonParameters)
                    AddCommonParameters(paramsBuffer);

                paramsBuffer.Remove(ADD_COMMON_PARAMETERS_FLAG);
            }

            Send(evt, paramsBuffer);
        }

        protected abstract void Send(string evt, Dictionary<string, object> paramsBuffer);

        private void AddCommonParameters(in Dictionary<string, object> paramsBuffer)
        {
            paramsBuffer.Add("days_since_reg", _daysSinceReg);
            paramsBuffer.Add("total_playtime_min", _sessionService.TotalPlayTimeMin);
            paramsBuffer.Add("total_playtime_sec", _sessionService.TotalPlayTimeSec);
            paramsBuffer.Add("days_in_game", _sessionService.DaysInGame);
            paramsBuffer.Add("session_number", _sessionNumber);
        }
    }
}