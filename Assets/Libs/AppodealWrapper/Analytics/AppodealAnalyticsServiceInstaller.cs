using Zenject;

namespace Libs.AppodealWrapper.Analytics
{
	public static class AppodealAnalyticsServiceInstaller
	{
		public static void Install(DiContainer container)
		{
			container.BindInitializableExecutionOrder<AppodealAnalyticInitializer>(-10);
			container.BindInterfacesAndSelfTo<AppodealAnalytic>().AsSingle().NonLazy();
			container.BindInterfacesAndSelfTo<AppodealAnalyticInitializer>().AsSingle().NonLazy();
		}
	}
}