using Ecs.Action.Commands.Turn;
using Ecs.Utils;
using Game.Utils.Movement;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems.Turn
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 85, nameof(EFeatures.Move))]
    public class EnterPreTurnReactAreaSystem : ForEachCommandUpdateSystem<EnterPreTurnReactAreaCommand>
    {
        private readonly GameContext _game;

        public EnterPreTurnReactAreaSystem(ICommandBuffer commandBuffer, GameContext game) : base(commandBuffer)
        {
            _game = game;
        }

        protected override void Execute(ref EnterPreTurnReactAreaCommand command)
        {
            // Более показать не могу
        }
    }
}