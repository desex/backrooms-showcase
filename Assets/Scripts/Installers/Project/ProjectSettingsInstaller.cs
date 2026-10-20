using Db.Advertisement;
using Db.Advertisement.Impl;
using Db.GameLevelsSettings.Impls;
using Db.GameLevelsSettings.Impls.Core;
using Db.Graphics;
using Db.Graphics.Impl;
using Db.Loading;
using Db.Loading.Impl;
using Db.SceneLoading;
using Db.SceneLoading.Impl;
using Db.Shaders;
using Db.Shaders.Impl;
using Db.SoundFxBase;
using Db.SoundFxBase.Impl;
using Playdarium.BuildPipelines.Runtime;
using Playdarium.BuildPipelines.Runtime.Impls;
using UnityEngine;
using Zenject;
using ZenjectUtil.Test.Extensions;

namespace Installers.Project
{
    [CreateAssetMenu(menuName = "Installers/" + nameof(ProjectSettingsInstaller), fileName = nameof(ProjectSettingsInstaller))]
    public class ProjectSettingsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private BuildSetting _buildSettings;
        [SerializeField] private ScenesBase _scenesBase;
        [SerializeField] private SoundFxBase _soundFxBase;
        [SerializeField] private UrpSettingsBase _urpSettingsBase;
        [SerializeField] private LoadingParameters _loadingParameters;
        [SerializeField] private GameLevelsResourcesPaths _gameLevelsResourcesPaths;
        [SerializeField] private AdInterstitialParameters _adInterstitialParameters;
        [SerializeField] private WarmupShadersBase _warmupShadersBase;
        
        public override void InstallBindings()
        {
            Container.Bind<IBuildSetting>().FromSubstitute(_buildSettings).AsSingle();
            Container.Bind<IScenesBase>().FromSubstitute(_scenesBase).AsSingle();
            Container.Bind<ISoundFxBase>().FromSubstitute(_soundFxBase).AsSingle();
            Container.Bind<IUrpSettingsBase>().FromSubstitute(_urpSettingsBase).AsSingle();
            Container.Bind<ILoadingParameters>().FromSubstitute(_loadingParameters).AsSingle();
            Container.Bind<IGameLevelsResourcesPaths>().FromInstance(_gameLevelsResourcesPaths).AsSingle();
            Container.Bind<IAdInterstitialParameters>().FromInstance(_adInterstitialParameters).AsSingle();
            Container.Bind<IWarmupShadersBase>().FromInstance(_warmupShadersBase).AsSingle();
        }
    }
}