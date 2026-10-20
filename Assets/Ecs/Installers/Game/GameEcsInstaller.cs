using Ecs.Installers.Game.Feature;
using Ecs.Installers.Game.Feature.Impls;
using Ecs.Utils.Groups.Impl;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Installers.Game
{
	public class GameEcsInstaller : AEcsInstaller
	{
		protected override void InstallSystems(Contexts contexts, bool isDebug)
		{
			BindGroups();
			
			Container.BindInterfacesTo<CommandBuffer>().AsSingle();
			
			BindContext<ActionContext>();
			BindContext<GameContext>();
			BindContext<InputContext>();
			BindContext<SchedulerContext>();
			BindContext<SignalContext>();

			GameEcsSystems.Install(Container, isDebug);

			// Event systems
			Container.BindInterfacesTo<ActionEventSystems>().AsSingle();
			Container.BindInterfacesTo<GameEventSystems>().AsSingle();
			Container.BindInterfacesTo<InputEventSystems>().AsSingle();
			Container.BindInterfacesTo<SchedulerEventSystems>().AsSingle();
			Container.BindInterfacesTo<SignalEventSystems>().AsSingle();

			//Cleanup system
			Container.BindInterfacesTo<ActionCleanupSystems>().AsSingle();
			Container.BindInterfacesTo<GameCleanupSystems>().AsSingle();
			Container.BindInterfacesTo<InputCleanupSystems>().AsSingle();
			Container.BindInterfacesTo<SchedulerCleanupSystems>().AsSingle();
			Container.BindInterfacesTo<SignalCleanupSystems>().AsSingle();
			
			BindFeature<GameFeature, IGameFeature>();
		}
		
		private void BindGroups()
		{
			Container.BindInterfacesTo<GameGroupUtils>().AsSingle();
		}
	}
}