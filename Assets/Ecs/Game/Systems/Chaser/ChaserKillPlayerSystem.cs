using System.Collections.Generic;
using Db.ChasePlayer;
using Ecs.Utils;
using Ecs.Utils.Revive;
using Game.Services.QTE;
using Game.Utils.Movement;
using Game.Utils.QTE;
using Game.Utils.Revive;
using Generated.Commands;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Game.Systems.Chaser
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 940, nameof(EFeatures.Chaser))]
    public class ChaserKillPlayerSystem : ReactiveSystem<GameEntity>
    {
        private readonly IChasePlayerParameters _chasePlayerParameters;
        private readonly ICommandBuffer _commandBuffer;
        private readonly GameContext _game;
        private readonly IQteService _qteService;

        public ChaserKillPlayerSystem(
            IContext<GameEntity> context,
            ICommandBuffer commandBuffer,
            IChasePlayerParameters chasePlayerParameters,
            GameContext game,
            IQteService qteService
        ) : base(context)
        {
            _commandBuffer = commandBuffer;
            _chasePlayerParameters = chasePlayerParameters;
            _game = game;
            _qteService = qteService;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.DistanceToPlayer.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.HasDistanceToPlayer && entity.DistanceToPlayer.Value <= 0;
        }

        protected override void Execute(IEnumerable<GameEntity> entities)
        {
            var player = _game.PlayerEntity;

            if (!player.IsDebugChaserDeath)
                return;

            if (!player.HasRevive)
                return;
            
            if (player.HasQteType)
                return;
            
            if (player.IsDead)
                return;
            
            if (player.HasObstaclePassReact)
                return;
            
            foreach (var chaser in entities)
            {
                var reviveData = player.Revive.Value;

                _game.PlayerEntity.ReplaceRevive(
                    new ReviveData
                    {
                        ReviveType = EReviveType.Teleport,
                        MovePoints = null,
                        ReviveTransform = reviveData.ReviveTransform,
                    }
                );

                if (_qteService.TryInitQte(EQteType.Chaser))
                    continue;

                _commandBuffer.KillPlayer(player.Uid.Value, EDeathReason.Obstacle, false);
                
                chaser.ReplaceDistanceToPlayer(_chasePlayerParameters.StartDistance);

                chaser.ReplaceForwardSpeed(
                    new ForwardSpeed(_chasePlayerParameters.StartChaserSpeed, _chasePlayerParameters.StartChaserSpeed)
                );
            }
        }
    }
}