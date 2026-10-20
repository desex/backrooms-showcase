using Ecs.Action.Commands;
using Ecs.Utils;
using Game.Utils.Movement;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 960, nameof(EFeatures.Move))]
    public class SetChunkCenterSystem : ForEachCommandUpdateSystem<SetChunkCenterCommand>
    {
        private readonly GameContext _game;

        public SetChunkCenterSystem(ICommandBuffer commandBuffer, GameContext game) : base(commandBuffer)
        {
            _game = game;
        }

        protected override void Execute(ref SetChunkCenterCommand command)
        {
            var character = _game.GetEntityWithUid(command.CharacterUid);

            var chunkCenter = command.Center;
            var isMoveAlongZ = !character.ChunkCenter.Value.IsMoveAlongZ;

            var centerValue = isMoveAlongZ ? chunkCenter.x : chunkCenter.z;

            character.ReplaceChunkCenter(new ChunkCenterParameters(isMoveAlongZ, centerValue));
        }
    }
}