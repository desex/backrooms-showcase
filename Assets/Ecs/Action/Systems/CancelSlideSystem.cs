using Ecs.Action.Commands;
using Ecs.Utils;
using Game.Services.Player;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 620, nameof(EFeatures.Move))]
    public class CancelSlideSystem : ForEachCommandUpdateSystem<CancelSlideCommand>
    {
        private readonly GameContext _game;
        private readonly IPlayerMovementTypeService _playerMovementTypeService;

        public CancelSlideSystem(
            ICommandBuffer commandBuffer,
            GameContext game,
            IPlayerMovementTypeService playerMovementTypeService
        ) : base(commandBuffer)
        {
            _game = game;
            _playerMovementTypeService = playerMovementTypeService;
        }

        protected override void Execute(ref CancelSlideCommand command)
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
            character.IsProtectedFromInstantKill = false;

            _playerMovementTypeService.UpdateBodyType(character.Uid.Value);
        }
    }
}