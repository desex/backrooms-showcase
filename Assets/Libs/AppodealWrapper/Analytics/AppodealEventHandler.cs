using System.Collections.Generic;
using AppodealStack.Monetization.Api;
using Core;

namespace Libs.AppodealWrapper.Analytics
{
    public class AppodealEventHandler : AParamsEventHandler
    {
        public AppodealEventHandler(
            int daysSinceReg,
            ISessionService sessionService,
            int sessionNumber
            // ILevelService levelService
        ) : base(
            daysSinceReg, 
            sessionService,
            sessionNumber
            // levelService
            )
        {
        }
        
        protected override void Send(string evt, Dictionary<string, object> paramsBuffer)
        {
            Appodeal.LogEvent(evt, paramsBuffer);
        }
    }
}