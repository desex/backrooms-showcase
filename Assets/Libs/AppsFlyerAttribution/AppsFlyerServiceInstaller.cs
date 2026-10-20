using Libs.AppsFlyerAttribution.Analytics;
using Zenject;

namespace Libs.AppsFlyerAttribution
{
	public static class AppsFlyerServiceInstaller
	{
		public static void Install(DiContainer container)
		{
			container.BindInitializableExecutionOrder<AppsFlyerInitializer>(-10);
			container.BindInterfacesAndSelfTo<AppsFlyerAnalytic>().AsSingle().NonLazy();
			container.BindInterfacesAndSelfTo<AppsFlyerInitializer>().AsSingle().NonLazy();
		}
	}
}