namespace Plugins.SimpleAdWrapper
{
    public struct ShowAdResult
    {
        public EAdType AdType;
        public string AdDealer;

        public bool WasShown;
        public bool WasSkipped;
        public bool WasFailed;

        public bool WasClicked;

        public override string ToString()
        {
            return $"{nameof(AdType)}: {AdType}, {nameof(AdDealer)}: {AdDealer}, {nameof(WasShown)}: {WasShown}, {nameof(WasSkipped)}: {WasSkipped}, {nameof(WasFailed)}: {WasFailed}, {nameof(WasClicked)}: {WasClicked}";
        }
    }
}