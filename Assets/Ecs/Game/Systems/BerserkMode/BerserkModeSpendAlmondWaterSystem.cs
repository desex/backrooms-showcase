using Db.BerserkMode;
using Ecs.Core.Interfaces;
using Ecs.Utils;
using Ecs.Utils.Groups;
using Ecs.Utils.ItemCollectables;
using Game.Services.CollectablesItem;
using Generated.Commands;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Game.Systems.BerserkMode
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 1006, nameof(EFeatures.Common))]
    public class BerserkModeSpendAlmondWaterSystem : APausableGameStateConditionUpdateSystem
    {
        private readonly IBerserkModeParameters _berserkModeParameters;
        private readonly ICollectiblesStorage _collectiblesStorage;
        private readonly ICommandBuffer _commandBuffer;
        private readonly IGameGroupUtils _gameGroupUtils;
        private readonly ITimeProvider _timeProvider;

        public BerserkModeSpendAlmondWaterSystem(
            ICommandBuffer commandBuffer,
            ICollectiblesStorage collectiblesStorage,
            IBerserkModeParameters berserkModeParameters,
            ITimeProvider timeProvider,
            GameContext game,
            IGameGroupUtils gameGroupUtils
        ) : base(game)
        {
            _commandBuffer = commandBuffer;
            _collectiblesStorage = collectiblesStorage;
            _berserkModeParameters = berserkModeParameters;
            _timeProvider = timeProvider;
            _gameGroupUtils = gameGroupUtils;
        }

        protected override bool GameStateCondition(EGameState gameState)
        {
            // Более показать не могу
            return default;
        }

        protected override bool Filter()
        {
            // Более показать не могу
            return default;
        }

        protected override void UpdateSystem()
        {
            // Более показать не могу
        }
    }
}