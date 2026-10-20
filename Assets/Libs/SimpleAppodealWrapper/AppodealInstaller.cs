using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;
using Libs.SimpleAppodealWrapper.Wrappers;
using Playdarium.BuildPipelines.Runtime;
using Plugins.SimpleAdWrapper.Emulator;
using Plugins.SimpleAdWrapper.Emulator.Settings;
using Plugins.SimpleAdWrapper.Emulator.Settings.Impl;
using Plugins.SimpleAdWrapper.Impl;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;
using ZenjectUtil.Test.Extensions;

namespace Libs.SimpleAppodealWrapper
{
    public class AppodealInstaller : MonoInstaller
    {
        [Title("For release")]
        [SerializeField] private AppodealSettings _settings;
        
        [Title("For editor")]
        [SerializeField] private AdEmulatorSettingsBase _settingsBase;
        [SerializeField] private AdStatesPatternBase _patternBase;
        
        [Inject] private IBuildSetting _buildSettings;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<RewardVideoAdService>().AsSingle();
            Container.BindInterfacesAndSelfTo<InterstitialAdService>().AsSingle();
#if UNITY_EDITOR
            InstallForEditor();
#else
            InstallForRelease();
#endif
        }

        private void InstallForRelease()
        {
            Appodeal.SetAutoCache(AppodealAdType.RewardedVideo, true);
            Appodeal.SetAutoCache(AppodealAdType.Interstitial, true);

            Appodeal.SetLogLevel(_buildSettings.BuildType == EBuildType.Release
                                     ? AppodealLogLevel.None
                                     : _settings.LogLevel
            );

            Container.Bind<AppodealSettings>().FromInstance(_settings);

            Container.BindInterfacesAndSelfTo<AppodealAdInitializer>().AsSingle();
            Container.BindInterfacesAndSelfTo<AppodealInterstitialAdWrapper>().AsSingle();
            Container.BindInterfacesAndSelfTo<AppodealRewardVideoAdWrapper>().AsSingle();
        }

        private void InstallForEditor()
        {
            if (!_settingsBase.useEmulator)
                return;

            Container.Bind<IAdStatesPatternBase>().FromSubstitute(_patternBase).AsSingle();
            Container.Bind<IAdEmulatorSettingsBase>().FromSubstitute(_settingsBase).AsSingle();

            Container.BindInterfacesAndSelfTo<EmulatorAdInitializer>().AsSingle();
            Container.BindInterfacesAndSelfTo<EmulatorInterstitialAdWrapper>().AsSingle();
            Container.BindInterfacesAndSelfTo<EmulatorRewardVideoAdWrapper>().AsSingle();
        }
    }
}