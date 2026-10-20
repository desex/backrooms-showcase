using Ecs.Action.Commands.Health;
using Ecs.Utils;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems.Health
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 969, nameof(EFeatures.Move))]
    public class RegenerateAfterDamageSystem : ForEachCommandUpdateSystem<RegenerateAfterDamageCommand>
    {
        private readonly GameContext _game;

        public RegenerateAfterDamageSystem(ICommandBuffer commandBuffer, GameContext game) : base(commandBuffer)
        {
            _game = game;
        }

        protected override void Execute(ref RegenerateAfterDamageCommand command)
        {
            var character = _game.GetEntityWithUid(command.CharacterUid);

            if (character.IsDead)
                return;

            character.IsDamaged = false;
        }
    }
}