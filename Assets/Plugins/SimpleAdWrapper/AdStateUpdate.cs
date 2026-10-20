namespace Plugins.SimpleAdWrapper
{
    public struct AdStateUpdate
    {
        public EAdState State;
        public EAdType AdType;
        public string AdDealer;
        public string Place;
        public double Duration;
        public string Error;
    }
}