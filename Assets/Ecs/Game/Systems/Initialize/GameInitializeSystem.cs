using Core.Utils;
using Ecs.Game.Extensions;
using Ecs.Utils;
using Game.Services.GameState;
using Game.Utils.Lights;
using Generated.Commands;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;
using PdUtils.PlayerPrefs;

namespace Ecs.Game.Systems.Initialize
{
    [Install(ExecutionType.Game, ExecutionPriority.High, 10, nameof(EFeatures.Initialization))]
    public class GameInitializeSystem : IInitializeSystem
    {
        private readonly ICommandBuffer _commandBuffer;
        private readonly GameContext _game;
        private readonly IGameStateService _gameStateService;
        private readonly IPlayerPrefsManager _playerPrefsManager;

        protected GameInitializeSystem(
            IGameStateService gameStateService,
            ICommandBuffer commandBuffer,
            GameContext game,
            IPlayerPrefsManager playerPrefsManager
        )
        {
            _gameStateService = gameStateService;
            _commandBuffer = commandBuffer;
            _game = game;
            _playerPrefsManager = playerPrefsManager;
        }

        public void Initialize()
        {
            // Более показать не могу
        }

        private void InitializePause()
        {
            // Более показать не могу
        }

        private void InitializeLight()
        {
            // Более показать не могу
        }

        private void InitializeLevel()
        {
            // Более показать не могу
        }

        private void InitializeTutorial(out bool isInTutorial)
        {
            // Более показать не могу
            isInTutorial = default;
        }
    }
}