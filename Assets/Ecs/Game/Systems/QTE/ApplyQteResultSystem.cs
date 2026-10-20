using Ecs.Action.Commands;
using Ecs.Utils;
using Game.Services.Player;
using Game.Utils.QTE;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;
using SimpleUi.Signals;
using Zenject;

namespace Ecs.Game.Systems.QTE
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 905 , nameof(EFeatures.Qte))]
    public class ApplyQteResultSystem : ForEachCommandUpdateSystem<ApplyQteResultCommand>
    {
        private readonly GameContext _game;
        private readonly SignalBus _signalBus;
        private readonly IPlayerMovementTypeService _playerMovementTypeService;

        public ApplyQteResultSystem(
            GameContext game,
            ICommandBuffer commandBuffer,
            SignalBus signalBus,
            IPlayerMovementTypeService playerMovementTypeService
        ) : base(commandBuffer)
        {
            _game = game;
            _signalBus = signalBus;
            _playerMovementTypeService = playerMovementTypeService;
        }

        protected override void Execute(ref ApplyQteResultCommand command)
        {
            // Более показать не могу
        }
    }
}