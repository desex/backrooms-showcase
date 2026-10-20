#if !DISABLE_GHOSTS
using Ecs.Core.Systems;
using Ecs.Utils;
using Game.Services.Ai;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;

namespace Ecs.Game.Systems.Ai
{
	[Install(ExecutionType.Game, ExecutionPriority.Normal, 1500, nameof(EFeatures.Ai))]
	public class AiBTreeSystem : APausableConditionUpdateSystem
	{
		private readonly IBehaviourTreesService _behaviourTreesService;

		public AiBTreeSystem(
			GameContext game,
			IBehaviourTreesService behaviourTreesService
		) : base(game)
		{
			_behaviourTreesService = behaviourTreesService;
		}

		protected override void UpdateSystem()
		{
			_behaviourTreesService.Update();
		}
	}
}
#endif