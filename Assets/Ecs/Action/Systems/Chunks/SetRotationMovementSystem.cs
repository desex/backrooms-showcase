using Ecs.Action.Commands.Movement;
using Ecs.Utils;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems.Chunks
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 950, nameof(EFeatures.Move))]
    public class SetTargetRotationSystem : ForEachCommandUpdateSystem<SetTargetRotationCommand>
    {
        private readonly GameContext _game;

        public SetTargetRotationSystem(ICommandBuffer commandBuffer, GameContext game) : base(commandBuffer)
        {
            _game = game;
        }

        protected override void Execute(ref SetTargetRotationCommand command)
        {
            var entity = _game.GetEntityWithUid(command.CharacterUid);

            if (entity.HasTurnChunkData)
                entity.RemoveTurnChunkData();

            if (entity.HasTurning)
                entity.RemoveTurning();

            var rotation = command.Rotation;
            entity.ReplaceTargetRotation(rotation);
        }
    }
}