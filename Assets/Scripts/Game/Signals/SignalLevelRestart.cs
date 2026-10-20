using Libs.Analytics;

namespace Game.Signals
{
    public class SignalLevelRestart : ISignalAnalytic
    {
        public readonly bool AddCommonParameters = true;
        
        public readonly int SessionRestartCount;
        public readonly int TotalRestartCount;

        public SignalLevelRestart(int sessionRestartCount, int totalRestartCount)
        {
            SessionRestartCount = sessionRestartCount;
            TotalRestartCount = totalRestartCount;
        }
    }
}