using Ecs.Action.Commands.Collectables;
using Ecs.Utils;
using Game.Services.CollectablesItem;
using Game.Services.Generation;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems.Collectables
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 50, nameof(EFeatures.CollectableItem))]
    public class CollectablesSpawnSystem : ForEachCommandUpdateSystem<CollectablesSpawnCommand>
    {
        private readonly ICollectableSpawnService _collectableSpawnService;
        private readonly IGenerationTasksHandler _generationTasksHandler;

        public CollectablesSpawnSystem(
            ICommandBuffer commandBuffer,
            ICollectableSpawnService collectableSpawnService,
            IGenerationTasksHandler generationTasksHandler
        ) : base(commandBuffer)
        {
            _collectableSpawnService = collectableSpawnService;
            _generationTasksHandler = generationTasksHandler;
        }

        protected override void Execute(ref CollectablesSpawnCommand command)
        {
            // Более показать не могу
        }
    }
}