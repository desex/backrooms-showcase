using Db.Player;
using Ecs.Game.Extensions;
using Ecs.Utils;
using Game.Providers;
using Game.Services.Player;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;
using Zenject;

namespace Ecs.Game.Systems.Initialize
{
    [Install(ExecutionType.Game, ExecutionPriority.High, 20, nameof(EFeatures.Initialization))]
    public class PlayerInitializeSystem : IInitializeSystem
    {
        private readonly DiContainer _diContainer;
        private readonly GameContext _game;
        private readonly IGameFieldProvider _gameFieldProvider;
        private readonly ILinkedEntityRepository _linkedEntityRepository;
        private readonly IPlayerHealthParametersBase _playerHealthParametersBase;
        private readonly IPlayerMovementTypeService _playerMovementTypeService;
        private readonly IPlayerSpeedProgressionParameters _playerSpeedProgressionParameters;

        public PlayerInitializeSystem(
            GameContext game,
            IPlayerMovementTypeService playerMovementTypeService,
            IPlayerHealthParametersBase playerHealthParametersBase,
            IPlayerSpeedProgressionParameters playerSpeedProgressionParameters,
            IGameFieldProvider gameFieldProvider,
            DiContainer diContainer,
            ILinkedEntityRepository linkedEntityRepository
        )
        {
            _game = game;
            _playerMovementTypeService = playerMovementTypeService;
            _playerHealthParametersBase = playerHealthParametersBase;
            _playerSpeedProgressionParameters = playerSpeedProgressionParameters;
            _gameFieldProvider = gameFieldProvider;
            _diContainer = diContainer;
            _linkedEntityRepository = linkedEntityRepository;
        }

        public void Initialize()
        {
            // Более показать не могу
        }
    }
}