using Ecs.Action.Commands.Movement;
using Ecs.Utils;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems.Movement
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 802, nameof(EFeatures.Move))]
    public class ChangeJumpHeightWithWideHoleSystem : ForEachCommandUpdateSystem<ChangeJumpHeightWithWideHoleCommand>
    {
        private readonly GameContext _game;

        public ChangeJumpHeightWithWideHoleSystem(ICommandBuffer commandBuffer, GameContext game) : base(commandBuffer)
        {
            _game = game;
        }

        protected override void Execute(ref ChangeJumpHeightWithWideHoleCommand command)
        {
            var character = _game.GetEntityWithUid(command.CharacterUid);

            if (command.IsEntering && !character.HasHoleDashTrigger)
            {
                character.IsInWideHole = true;
                return;
            }

            character.IsComeback = false;
            character.IsInWideHole = false;
        }
    }
}