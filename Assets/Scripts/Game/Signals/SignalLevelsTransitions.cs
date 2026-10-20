using Libs.Analytics;

namespace Game.Signals
{
    public class SignalLevelsTransitions : ISignalAnalytic
    {
        public readonly bool AddCommonParameters = true;

        public readonly int TransitionsCountInRun;
        public readonly int TransitionsCountAll;
        public readonly string TransitionsLevels;

        public SignalLevelsTransitions(
            int transitionsCountInRun,
            int transitionsCountAll,
            string transitionsLevels
        )
        {
            TransitionsCountInRun = transitionsCountInRun;
            TransitionsCountAll = transitionsCountAll;
            TransitionsLevels = transitionsLevels;
        }
    }
}