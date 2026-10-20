using Ecs.Action.Commands;
using Ecs.Utils;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems.Health
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 950, nameof(EFeatures.Move))]
    public class SetPlayerCanBeDamagedSystem : ForEachCommandUpdateSystem<SetPlayerCanBeDamagedCommand>
    {
        private readonly GameContext _game;

        public SetPlayerCanBeDamagedSystem(ICommandBuffer commandBuffer, GameContext game) : base(commandBuffer)
        {
            _game = game;
        }

        protected override void Execute(ref SetPlayerCanBeDamagedCommand command)
        {
            // Более показать не могу
        }
    }
}