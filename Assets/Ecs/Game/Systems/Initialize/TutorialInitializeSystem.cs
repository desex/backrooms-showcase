using Ecs.Utils;
using Game.Services.Render;
using Game.Services.Tutorial;
using Game.Services.TutorialStateMachine;
using Game.Utils.MovementBlock;
using Game.Utils.Render;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;

namespace Ecs.Game.Systems.Initialize
{
    [Install(ExecutionType.Game, ExecutionPriority.High, 20, nameof(EFeatures.Initialization))]
    public class TutorialInitializeSystem : IInitializeSystem
    {
        private readonly GameContext _game;
        private readonly ITutorialSequencesProvider _tutorialSequencesProvider;
        private readonly ITutorialStateMachine _tutorialStateMachine;
        private readonly ITvLinesService _tvLinesService;
        private readonly IWhiteNoiseService _whiteNoiseService;

        public TutorialInitializeSystem(
            GameContext game,
            ITutorialStateMachine tutorialStateMachine,
            ITvLinesService tvLinesService,
            ITutorialSequencesProvider tutorialSequencesProvider,
            IWhiteNoiseService whiteNoiseService
        )
        {
            _game = game;
            _tutorialStateMachine = tutorialStateMachine;
            _tvLinesService = tvLinesService;
            _tutorialSequencesProvider = tutorialSequencesProvider;
            _whiteNoiseService = whiteNoiseService;
        }

        public void Initialize()
        {
            // Более показать не могу
        }
    }
}