using Ecs.Action.Commands;
using Ecs.Utils;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 625, nameof(EFeatures.Move))]
    public class SetReadyFallDamageSystem : ForEachCommandUpdateSystem<SetReadyFallDamageCommand>
    {
        private readonly GameContext _game;

        public SetReadyFallDamageSystem(ICommandBuffer commandBuffer, GameContext game) : base(commandBuffer)
        {
            _game = game;
        }

        protected override void Execute(ref SetReadyFallDamageCommand command)
        {
            // Более показать не могу
        }
    }
}