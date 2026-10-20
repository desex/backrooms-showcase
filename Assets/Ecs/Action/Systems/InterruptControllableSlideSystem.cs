using Ecs.Action.Commands;
using Ecs.Utils;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 595, nameof(EFeatures.Move))]
    public class InterruptControllableSlideSystem : ForEachCommandUpdateSystem<InterruptControllableSlideCommand>
    {
        private readonly GameContext _game;

        public InterruptControllableSlideSystem(ICommandBuffer commandBuffer, GameContext game) : base(commandBuffer)
        {
            _game = game;
        }

        protected override void Execute(ref InterruptControllableSlideCommand command)
        {
            var character = _game.GetEntityWithUid(command.CharacterUid);

            if (character.HasSlideDuration)
                character.RemoveSlideDuration();

            if (character.HasControllableSlideDirection)
                character.RemoveControllableSlideDirection();

            if (!character.HasSliding)
                return;

            character.RemoveSliding();
            character.IsCanBeDamaged = true;
            character.IsUseExtraGravity = false;
            character.IsCanSwipe = true;
            character.IsControllableSlideInterrupted = true;
        }
    }
}