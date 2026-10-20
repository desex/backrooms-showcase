using Ecs.Action.Commands;
using Ecs.Utils;
using Game.Services.Player;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 590, nameof(EFeatures.Move))]
    public class EndPlankRunSystem : ForEachCommandUpdateSystem<EndPlankRunCommand>
    {
        private readonly GameContext _game;
        private readonly IPlayerMovementTypeService _playerMovementTypeService;

        public EndPlankRunSystem(
            ICommandBuffer commandBuffer,
            GameContext game,
            IPlayerMovementTypeService playerMovementTypeService
        ) : base(commandBuffer)
        {
            _game = game;
            _playerMovementTypeService = playerMovementTypeService;
        }

        protected override void Execute(ref EndPlankRunCommand command)
        {
            var player = _game.GetEntityWithUid(command.CharacterUid);

            if (player.HasPlankRun)
                player.RemovePlankRun();

            if (player.HasPlankRunCentralizeDuration)
                player.RemovePlankRunCentralizeDuration();

            player.IsCanSwipe = true;
            player.IsCanBeDamaged = true;

            _playerMovementTypeService.UpdateBodyType(command.CharacterUid);
        }
    }
}