using Libs.Analytics;

namespace Game.Signals
{
    public class SignalRevive : ISignalAnalytic
    {
        public readonly bool AddCommonParameters = true;

        public readonly int ReviveCount;

        public SignalRevive(int reviveCount)
        {
            ReviveCount = reviveCount;
        }
    }
}