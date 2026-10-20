using Ecs.Action.Commands.Turn;
using Ecs.Utils;
using Generated.Commands;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems.Turn
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 90, nameof(EFeatures.Move))]
    public class TryAutomaticTurnSystem : ForEachCommandUpdateSystem<TryAutomaticTurnCommand>
    {
        private readonly ICommandBuffer _commandBuffer;
        private readonly GameContext _game;

        public TryAutomaticTurnSystem(ICommandBuffer commandBuffer, GameContext game) : base(commandBuffer)
        {
            _commandBuffer = commandBuffer;
            _game = game;
        }

        protected override void Execute(ref TryAutomaticTurnCommand command)
        {
            var player = _game.PlayerEntity;

            if (!player.IsPreTurnReact)
                return;

            _commandBuffer.StartPlayerTurn(player.Uid.Value);
            player.IsPreTurnReact = false;
        }
    }
}