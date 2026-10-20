using System.Threading;
using Cysharp.Threading.Tasks;
using Ecs.Action.Commands.Collectables;
using Ecs.Managers;
using Ecs.Utils;
using Game.Services.CollectablesItem;
using Game.Services.Generation;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems.Collectables
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 575, nameof(EFeatures.CollectableItem))]
    public class CollectiblesSelectionSystem : ForEachCommandUpdateSystem<CollectablesSelectionCommand>
    {
        private readonly ICollectableSpawnService _collectableSpawnService;
        private readonly ICollectiblesStorage _collectiblesStorage;
        private readonly GameContext _game;
        private readonly IGenerationTasksHandler _generationTasksHandler;

        public CollectiblesSelectionSystem(
            ICommandBuffer commandBuffer,
            ICollectiblesStorage collectiblesStorage,
            ICollectableSpawnService collectableSpawnService,
            IGenerationTasksHandler generationTasksHandler,
            GameContext game
        ) : base(commandBuffer)
        {
            _collectiblesStorage = collectiblesStorage;
            _collectableSpawnService = collectableSpawnService;
            _generationTasksHandler = generationTasksHandler;
            _game = game;
        }

        protected override void Execute(ref CollectablesSelectionCommand command)
        {
            // Более показать не могу
        }

        private void SelectCollectables(Uid collectableId)
        {
            // Более показать не могу
        }

        private async UniTask DespawnCollectablesAsync(Uid collectableId, CancellationToken token)
        {
            // Более показать не могу
        }
    }
}