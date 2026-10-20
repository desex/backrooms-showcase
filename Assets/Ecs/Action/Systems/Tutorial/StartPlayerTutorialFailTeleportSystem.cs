using Ecs.Action.Commands.Tutorial;
using Ecs.Utils;
using Game.UI;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;
using SimpleUi.Signals;
using Zenject;

namespace Ecs.Action.Systems.Tutorial
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 85, nameof(EFeatures.Tutorial))]
    public class StartPlayerTutorialFailTeleportSystem
        : ForEachCommandUpdateSystem<StartPlayerTutorialFailTeleportCommand>
    {
        private readonly GameContext _game;
        private readonly SignalBus _signalBus;

        public StartPlayerTutorialFailTeleportSystem(
            ICommandBuffer commandBuffer,
            GameContext game,
            SignalBus signalBus
        ) : base(commandBuffer)
        {
            _game = game;
            _signalBus = signalBus;
        }

        protected override void Execute(ref StartPlayerTutorialFailTeleportCommand command)
        {
            // Более показать не могу
        }
    }
}