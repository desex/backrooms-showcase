using UnityEngine;

namespace Db.Advertisement.Impl
{
    [CreateAssetMenu(menuName = "Settings/Advertisement/" + nameof(AdInterstitialParameters), fileName = nameof(AdInterstitialParameters))]
    public class AdInterstitialParameters : ScriptableObject, IAdInterstitialParameters
    {
        [field: SerializeField] public bool ShouldShowInterstitial { get; private set; } = true;
        [field: SerializeField] public int InterstitialCooldownSec { get; private set; } = 60;
    }
}