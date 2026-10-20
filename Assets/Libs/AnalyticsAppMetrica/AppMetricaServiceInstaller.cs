using Zenject;

namespace Libs.AnalyticsAppMetrica
{
	public class AppMetricaServiceInstaller
	{
		public static void Install(DiContainer container)
		{
			container.BindInitializableExecutionOrder<AppMetricaInitializer>(-10);
			container.BindInterfacesAndSelfTo<AppMetrica>().AsSingle().NonLazy();
			container.BindInterfacesAndSelfTo<AppMetricaInitializer>().AsSingle().NonLazy();
		}
	}
}