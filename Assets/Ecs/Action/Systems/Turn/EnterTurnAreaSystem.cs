using Ecs.Action.Commands.Turn;
using Ecs.Utils;
using Game.Utils.Movement;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems.Turn
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 90, nameof(EFeatures.Move))]
    public class EnterTurnAreaSystem : ForEachCommandUpdateSystem<EnterTurnAreaCommand>
    {
        private readonly GameContext _game;

        public EnterTurnAreaSystem(ICommandBuffer commandBuffer, GameContext game) : base(commandBuffer)
        {
            _game = game;
        }

        protected override void Execute(ref EnterTurnAreaCommand command)
        {
            // Более показать не могу
        }
    }
}