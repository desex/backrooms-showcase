using Libs.Analytics;

namespace Game.Signals
{
    public class SignalLevelFinish : ISignalAnalytic
    {
        public readonly bool AddCommonParameters = true;
        
        public int TimeSpentSec;
        public int ScoreGained;
        public int DistanceTraveled;
        public int AverageFps;

        public SignalLevelFinish(
            int timeSpentSec,
            int scoreGained,
            int distanceTraveled,
            int averageFps
        )
        {
            TimeSpentSec = timeSpentSec;
            ScoreGained = scoreGained;
            DistanceTraveled = distanceTraveled;
            AverageFps = averageFps;
        }
    }
}