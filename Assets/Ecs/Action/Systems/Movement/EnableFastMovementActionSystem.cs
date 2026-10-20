using Ecs.Action.Commands.Movement;
using Ecs.Utils;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems.Movement
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 800, nameof(EFeatures.Move))]
    public sealed class EnableFastMovementActionSystem : ForEachCommandUpdateSystem<EnableFastMovementActionCommand>
    {
        private readonly GameContext _game;

        public EnableFastMovementActionSystem(ICommandBuffer commandBuffer, GameContext game) : base(commandBuffer)
        {
            _game = game;
        }

        protected override void Execute(ref EnableFastMovementActionCommand command)
        {
            var character = _game.GetEntityWithUid(command.CharacterUid);

            if (!CanHandle(character))
                return;

            character.IsFastActionMove = true;
            character.IsReadyForFastActionMove = false;
        }

        private static bool CanHandle(GameEntity character)
        {
            return character.IsReadyForFastActionMove && !character.IsFastActionMove && !character.IsDestroyed;
        }
    }
}