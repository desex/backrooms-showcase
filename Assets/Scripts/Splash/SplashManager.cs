#if UNITY_IPHONE && !UNITY_EDITOR
using AppodealAppTracking.Unity.Api;
using AppsFlyerSDK;
#endif
using AppodealAppTracking.Unity.Common;
using Game.Services.SceneLoading;
using Game.Utils.UniTaskExtensions;
using Plugins.SimpleAdWrapper;
using UnityEngine;
using Zenject;

namespace Splash
{
	public class SplashManager : MonoBehaviour, IAppodealAppTrackingTransparencyListener
	{
		[Inject]
		private  ISceneLoadingManager _sceneLoadingManager;
		
		[Inject]
		private IAdInitializer _adInitializer;

		public void Start()
		{
#if !UNITY_EDITOR
            Debug.unityLogger.logEnabled = false;
#endif
            
#if UNITY_IPHONE && !UNITY_EDITOR		
			AppsFlyer.waitForATTUserAuthorizationWithTimeoutInterval(60);
			AppodealAppTrackingTransparency.RequestTrackingAuthorization(this);
#else
			_adInitializer.InitializeAsync().ForgetAndWrapExceptions();	
#endif
			
			_sceneLoadingManager.LoadGameFromSplash();
		}

		public void AppodealAppTrackingTransparencyListenerNotDetermined()
		{
			// Более показать не могу
		}

		public void AppodealAppTrackingTransparencyListenerRestricted()
		{
			// Более показать не могу
		}

		public void AppodealAppTrackingTransparencyListenerDenied()
		{
			// Более показать не могу
		}

		public void AppodealAppTrackingTransparencyListenerAuthorized()
		{
			// Более показать не могу
		}
	}
}