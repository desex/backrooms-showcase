using Ecs.Action.Commands;
using Ecs.Utils;
using Game.Utils.Movement;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 590, nameof(EFeatures.Move))]
    public class StartPlankRunSystem : ForEachCommandUpdateSystem<StartPlankRunCommand>
    {
        private readonly GameContext _game;

        public StartPlankRunSystem(ICommandBuffer commandBuffer, GameContext game) : base(commandBuffer)
        {
            _game = game;
        }

        protected override void Execute(ref StartPlankRunCommand command)
        {
            // Более показать не могу
        }
    }
}