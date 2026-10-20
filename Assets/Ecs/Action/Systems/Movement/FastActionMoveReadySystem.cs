using Ecs.Action.Commands.Movement;
using Ecs.Utils;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems.Movement
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 90, nameof(EFeatures.Move))]
    public class FastActionMoveReadySystem : ForEachCommandUpdateSystem<FastActionMoveReadyCommand>
    {
        private readonly GameContext _game;

        public FastActionMoveReadySystem(ICommandBuffer commandBuffer, GameContext game) : base(commandBuffer)
        {
            _game = game;
        }

        protected override void Execute(ref FastActionMoveReadyCommand readyCommand)
        {
            var character = _game.GetEntityWithUid(readyCommand.CharacterUid);

            if (character.IsDamaged)
                return;

            character.IsReadyForFastActionMove = true;
        }
    }
}