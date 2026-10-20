using System;

namespace Libs.AnalyticsAppMetrica.Utils
{
    [Serializable]
    public struct Receipt
    {
        public string Store;
        public string TransactionID;
        public string Payload;
    }
}