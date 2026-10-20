using Db.ChasePlayer;
using Db.Monsters;
using Db.Player;
using Db.SoundFxBase;
using Ecs.Game.Extensions;
using Ecs.Utils;
using Game.Services.Sound;
using Game.Utils.Movement;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;

namespace Ecs.Game.Systems.Initialize
{
    [Install(ExecutionType.Game, ExecutionPriority.High, 25, nameof(EFeatures.Initialization))]
    public class ChaserInitializeSystem : IInitializeSystem
    {
        private readonly IChasePlayerParameters _chasePlayerParameters;
        private readonly IChaserPresentationParameters _chaserPresentationParameters;
        private readonly GameContext _game;
        private readonly IPlayerHealthParametersBase _playerHealthParametersBase;
        private readonly ISoundService _soundService;

        public ChaserInitializeSystem(
            IChaserPresentationParameters chaserPresentationParameters,
            IChasePlayerParameters chasePlayerParameters,
            IPlayerHealthParametersBase playerHealthParametersBase,
            ISoundService soundService,
            GameContext game
        )
        {
            _chaserPresentationParameters = chaserPresentationParameters;
            _game = game;
            _chasePlayerParameters = chasePlayerParameters;
            _playerHealthParametersBase = playerHealthParametersBase;
            _soundService = soundService;
        }

        public void Initialize()
        {
            // Более показать не могу
        }

        private float CalculateAdditionalSpeed()
        {
            // Более показать не могу
            return default;
        }
    }
}