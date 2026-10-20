#if !DISABLE_GHOSTS
using System.Collections.Generic;
using Db.Ai;
using Ecs.Utils;
using Game.Models.Ai;
using Game.Utils.Ai;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;
using Zenject;

namespace Ecs.Game.Systems.Ai
{
	[Install(ExecutionType.Game, ExecutionPriority.Normal, 200, nameof(EFeatures.Ai))]
	public class AiInitializeSystem : ReactiveSystem<GameEntity>
	{
		private readonly IAiBTreeSettingsBase _aiBTreeSettingsBase;
		private readonly DiContainer _container;
		private readonly GameContext _game;

		public AiInitializeSystem(
			GameContext game,
			IAiBTreeSettingsBase aiBTreeSettingsBase,
			DiContainer container
		) : base(game)
		{
			_aiBTreeSettingsBase = aiBTreeSettingsBase;
			_container = container;
			_game = game;
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
			=> context.CreateCollector(GameMatcher.BTree);

		protected override bool Filter(GameEntity entity)
			=> entity.IsAi && entity.HasAiType && entity.HasBTree && !entity.IsDestroyed;
		
		protected override void Execute(IEnumerable<GameEntity> entities)
		{
			var isGameStarted = _game.GameStateEntity.GameState.Value == EGameState.Game;
			
			foreach (var entity in entities)
			{
				var aiType = entity.AiType.Value;
				var externalBehaviorTree = _aiBTreeSettingsBase.Trees[aiType];
				var bTree = entity.BTree.Value;

				bTree.ExternalBehavior = externalBehaviorTree;

				bTree.QueueAllTasksForInject(_container);
				
				var uid = entity.Uid.Value;
				var sharedUid = bTree.GetVariable(TaskPropertiesNames.SELF_UID_PROPERTY_NAME);
				sharedUid.SetValue(uid);
				
				entity.IsAiEnabled = isGameStarted;
			}
		}
	}
}
#endif