using AppodealStack.Monetization.Common;
using Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Libs.SimpleAppodealWrapper
{
	[CreateAssetMenu(fileName = nameof(AppodealSettings), menuName = "Monetization/" + nameof(AppodealSettings))]
	public class AppodealSettings : ScriptableObject
	{
		[Title("API keys")]
		[SerializeField] private string googleApiKey;
		[SerializeField] private string appleApiKey;
		
		[Title("Ad types")]
		[SerializeField] private bool _interstitial;
		[SerializeField] private bool _banner;
		[SerializeField] private bool _rewardVideo;
		[SerializeField] private bool _mrec;

		[field: Title("Timings")]
		[field: SerializeField] public int InterstitialLoadTimeSec { get; private set; }
		[field: SerializeField] public int RewardVideoLoadTimeSec { get; private set; }
		
		[field: Title("Log")]
		[field: SerializeField] public AppodealLogLevel LogLevel { get; private set; }
		
		public string ApiKey => PlatformUtils.IsIPhone() ? appleApiKey : googleApiKey;
		
		public int AdIntValue
		{
			get
			{
				var adType = AppodealAdType.None;
				
				if (_interstitial)
					adType |= AppodealAdType.Interstitial;

				if (_banner)
					adType |= AppodealAdType.Banner;
				
				if (_rewardVideo)
					adType |= AppodealAdType.RewardedVideo;
				
				if (_mrec)
					adType |= AppodealAdType.Mrec;
				
				return adType;
			}
		}
	}
}