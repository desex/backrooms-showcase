using System.Collections.Generic;
using Core;
using UnityEngine;
using Zenject;

namespace Libs.AnalyticsAppMetrica
{
	public class AppMetricaInstaller : MonoInstaller
	{
		[SerializeField] private global::AppMetrica prefabIos;
		[SerializeField] private global::AppMetrica prefabAndroid;

		public override void InstallBindings()
		{
			if (PlatformUtils.IsIPhone())
			{
				Instantiate(prefabIos);
			}

			if (PlatformUtils.IsAndroid())
			{
				Instantiate(prefabAndroid);
			}

			Container.Bind<IYandexAppMetrica>().FromInstance(global::AppMetrica.Instance).AsSingle();
			Container.Bind<LoggerSetAppMetricaId>().AsSingle().NonLazy();
		}

		private class LoggerSetAppMetricaId
		{
			private readonly IYandexAppMetrica _appMetrica;

			public LoggerSetAppMetricaId(IYandexAppMetrica appMetrica)
			{
				_appMetrica = appMetrica;

				appMetrica.RequestAppMetricaDeviceID(OnAppMetricaId);
			}

			private void OnAppMetricaId(string id, YandexAppMetricaRequestDeviceIDError? error)
			{
				if (error.HasValue)
				{
					Debug.LogError($"[{nameof(AppMetricaInstaller)}] {error}");
					return;
				}

				_appMetrica.ReportEvent("user", new Dictionary<string, object>
				{
					{ "device_id", id }
				});
			}
		}
	}
}