using AppodealAppTracking.Unity.Common;
using Playdarium.BuildPipelines.Runtime;
using Zenject;

namespace Libs.AppsFlyerAttribution
{
    //TODO: NEED CHECK maybe this is does not need
    public class AppsFlyerInstaller : MonoInstaller, IAppodealAppTrackingTransparencyListener
    {
        public AppsFlyerObjectScript appsFlyerPrefab;

        [Inject]
        private IBuildSetting _buildSetting;

        public override void InstallBindings()
        {
            
        }

        public void AppodealAppTrackingTransparencyListenerNotDetermined()
        {
            InitializeAppsflyer();
        }

        public void AppodealAppTrackingTransparencyListenerRestricted()
        {
            InitializeAppsflyer();
        }

        public void AppodealAppTrackingTransparencyListenerDenied()
        {
            InitializeAppsflyer();
        }

        public void AppodealAppTrackingTransparencyListenerAuthorized()
        {
            InitializeAppsflyer();
        }
        
        private void InitializeAppsflyer()
        {
            var appsFlyerObj = Instantiate(appsFlyerPrefab);
            DontDestroyOnLoad(appsFlyerObj.gameObject);
            appsFlyerObj.Initialize(_buildSetting.BuildType == EBuildType.Debug);

        }
    }
}