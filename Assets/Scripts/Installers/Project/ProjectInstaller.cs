using Core;
using Core.Utils;
using Core.Utils.Proxifier;
using Game.DaoData;
using Game.DaoData.Collectibles;
using Game.DaoData.Tutorial;
using Game.Providers.Impls;
using Game.Services.LevelResources.Impl;
using Game.Services.Providers;
using Game.Services.Providers.impl;
using Game.Services.Providers.impl.GenerationSettingsProviders;
using Game.Services.Render.Impl;
using Game.Services.SceneLoading;
using Game.Services.SceneLoading.Impl;
using Game.Services.Start.Impl;
using Game.Signals;
using PdUtils.Dao;
using PdUtils.DateTimeService;
using PdUtils.DateTimeService.Impl;
using PdUtils.FirstStartService;
using PdUtils.FirstStartService.Impl;
using PdUtils.GeoLocation;
using PdUtils.GeoLocation.Impl;
using PdUtils.MailService.Impl;
using PdUtils.PlayerPrefs;
using PdUtils.PlayerPrefs.Impl;
using PdUtils.RandomProvider.Impl;
using PdUtils.SceneLoadingProcessor.Impls;
using PdUtils.Vibration.Impl;
using PdUtils.Web.Impl;
using Project;
using Project.Services.Advertisement;
using Project.Services.Advertisement.Impl;
using UnityEngine;
using Zenject;
using ZenjectUtil.Test.Extensions;

namespace Installers.Project
{
	public class ProjectInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Application.targetFrameRate = 60;

			Container.BindInterfacesTo<UnityRandomProvider>().AsSingle();
			Container.BindInterfacesTo<LevelResourcesLoader>().AsSingle();
			Container.BindInterfacesTo<LoadingProcessor>().AsSingle();

			Container.BindSubstituteInterfacesTo<ISceneLoadingManager, SceneLoadingManager>().AsSingle();

			Container.BindFromSubstitute<IPlayerPrefsManager, PersistancePlayerPrefsManager>().AsSingle();

			Container.BindFromSubstitute<IDateTimeService, DateTimeService>().AsSingle();

			Container.BindFromSubstitute<IFirstStartService, FirstStartService>().AsSingle();

			Container.BindInterfacesAndSelfTo<VibrationService>().AsSingle();

			SignalBusInstaller.Install(Container);

			Container.BindInterfacesTo<GeoLocationService>().AsSingle();

			Container.BindInterfacesAndSelfTo<WebRequester<LocationVo>>().AsTransient();
			Container.BindInterfacesAndSelfTo<WebRequester<string>>().AsTransient();
			Container.Bind<GeoLocationService.Settings>()
			         .FromInstance(new GeoLocationService.Settings(24L))
			         .AsSingle()
			         .WhenInjectedInto<GeoLocationService>();

			Container.BindInterfacesTo<SessionService>().AsSingle().NonLazy();

			Container.BindInterfacesAndSelfTo<MailService>().AsSingle();
			Container.BindInterfacesTo<ProjectWindowManager>().AsSingle().NonLazy();
			
			Container.BindInterfacesTo<GameStartService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<RendererFeatureProvider>().AsSingle();
			Container.BindInterfacesAndSelfTo<GlitchService>().AsSingle();
			Container.BindInterfacesAndSelfTo<TvLinesService>().AsSingle();
            Container.BindInterfacesAndSelfTo<WhiteNoiseService>().AsSingle();
			
			BindSignals();
			 
			BindDao();
			
			Container.BindFromSubstitute<IGenerationResourcesProvider, GenerationResourcesProvider>().AsSingle();
			Container.BindFromSubstitute<IGlobalIlluminationResourcesProvider, GlobalIlluminationResourcesProvider>().AsSingle();
			Container.BindFromSubstitute<IInterstitialAvailableService, InterstitialAvailableService>().AsSingle();
		}

		private void BindSignals()
		{
			Container.DeclareSignal<SignalProxyBefore>().OptionalSubscriber();
			Container.DeclareSignal<SignalProxyAfter>().OptionalSubscriber();
			Container.DeclareSignal<SignalReportAdRevenue>();
		}
		
		private void BindDao()
		{
			Container.BindFromSubstitute<IDao<LevelScoresData>, LocalStorageDao<LevelScoresData>>()
			         .AsSingle()
			         .WithArguments(DaoSavePathKeys.SCORES);
			Container.BindFromSubstitute<IDao<SettingsData>, LocalStorageDao<SettingsData>>()
			         .AsSingle()
			         .WithArguments(DaoSavePathKeys.SETTINGS);
			Container.BindFromSubstitute<IDao<CollectiblesStorageData>, LocalStorageDao<CollectiblesStorageData>>()
			         .AsSingle()
			         .WithArguments(DaoSavePathKeys.COLLECTIBLES);
			Container.BindFromSubstitute<IDao<TutorialData>, LocalStorageDao<TutorialData>>()
			         .AsSingle()
			         .WithArguments(DaoSavePathKeys.TUTORIAL);
		}
		
		public override void Start()
		{
			Debug.Log("device ID = " + SystemInfo.deviceUniqueIdentifier);
		}
	}
}