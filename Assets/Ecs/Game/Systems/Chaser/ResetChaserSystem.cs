using Db.ChasePlayer;
using Ecs.Action.Commands.Health;
using Ecs.Utils;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Game.Systems.Chaser
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 667, nameof(EFeatures.Common))]
    public class ResetChaserSystem : ForEachCommandUpdateSystem<ReviveCommand>
    {
        private readonly IChasePlayerParameters _chasePlayerParameters;
        private readonly GameContext _game;

        public ResetChaserSystem(
            ICommandBuffer commandBuffer,
            GameContext game,
            IChasePlayerParameters chasePlayerParameters
        ) : base(commandBuffer)
        {
            _game = game;
            _chasePlayerParameters = chasePlayerParameters;
        }

        protected override void Execute(ref ReviveCommand command)
        {
            var chaser = _game.ChaserEntity;
            var player = _game.PlayerEntity;
            var afterQteLeftTime = _chasePlayerParameters.AfterQteLeftTime;
            var reviveCharacter = _game.GetEntityWithUid(command.TargetUid);
            
            if (!reviveCharacter.IsPlayer)
                return;
            
            player.ReplaceAfterQteLeftTime(afterQteLeftTime);
            chaser.IsInAfterQteSlowdown = true;
        }
    }
}