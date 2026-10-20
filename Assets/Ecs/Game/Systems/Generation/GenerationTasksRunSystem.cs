using Ecs.Utils;
using Game.Services.Generation;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;

namespace Ecs.Game.Systems.Generation
{
    [Install(ExecutionType.Game, ExecutionPriority.High, 100, nameof(EFeatures.Generation))]
    public class GenerationTasksRunSystem : IUpdateSystem
    {
        private readonly IGenerationTasksRunner _generationTasksRunner;

        public GenerationTasksRunSystem(IGenerationTasksRunner generationTasksRunner)
        {
            _generationTasksRunner = generationTasksRunner;
        }

        public void Update()
        {
            _generationTasksRunner.TryRunTasks();
        }
    }
}