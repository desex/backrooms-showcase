using Db.QTE;
using Ecs.Action.Commands;
using Ecs.Utils;
using Game.Services.Player;
using Game.UI;
using Game.Utils.QTE;
using Generated.Commands;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;
using SimpleUi.Signals;
using UnityEngine;
using Zenject;

namespace Ecs.Game.Systems.QTE
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 890, nameof(EFeatures.Qte))]
    public class StartQteSystem : ForEachCommandUpdateSystem<StartQteCommand>
    {
        private readonly ICommandBuffer _commandBuffer;
        private readonly SignalBus _signalBus;
        private readonly GameContext _game;
        private readonly IQteParameters _qteParameters;
        private readonly IPlayerMovementTypeService _playerMovementTypeService;

        public StartQteSystem(
            ICommandBuffer commandBuffer, 
            SignalBus signalBus,
            GameContext game,
            IQteParameters qteParameters,
            IPlayerMovementTypeService playerMovementTypeService
        ) : base(commandBuffer)
        {
            _commandBuffer = commandBuffer;
            _signalBus = signalBus;
            _game = game;
            _qteParameters = qteParameters;
            _playerMovementTypeService = playerMovementTypeService;
        }

        protected override void Execute(ref StartQteCommand command)
        {
            // Более показать не могу
        }

        private float CalculateQteTime(GameEntity player)
        {
            // Более показать не могу
            return default;
        }
        
        private void HideObstaclesInPlayerForward(GameEntity player)
        {
            // Более показать не могу
        }
    }
}