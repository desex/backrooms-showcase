using Libs.Analytics;

namespace Game.Signals
{
    public struct SignalFpsDecrease : ISignalAnalytic
    {
        public int Fps;
        public int Duration;

        public SignalFpsDecrease(int fps, int duration)
        {
            Fps = fps;
            Duration = duration;
        }
    }
}