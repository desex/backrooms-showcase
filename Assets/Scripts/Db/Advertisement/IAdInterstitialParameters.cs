namespace Db.Advertisement
{
    public interface IAdInterstitialParameters
    {
        bool ShouldShowInterstitial { get; }
        int InterstitialCooldownSec { get; }
    }
}