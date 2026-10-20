using System.Collections.Generic;
using Db.Player;
using Ecs.Utils;
using Game.Utils.Movement;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;

namespace Ecs.Game.Systems.Movement
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 585, nameof(EFeatures.Move))]
    public class PlayerAfterDeathMovementSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext _game;
        private readonly IPlayerDeathParametersBase _deathParametersBase;
        private readonly IPlayerWaterInHoleParametersBase _playerWaterInHoleParametersBase;

        public PlayerAfterDeathMovementSystem(
            GameContext game,
            IPlayerDeathParametersBase deathParametersBase,
            IPlayerWaterInHoleParametersBase playerWaterInHoleParametersBase
        ) : base(game)
        {
            _game = game;
            _deathParametersBase = deathParametersBase;
            _playerWaterInHoleParametersBase = playerWaterInHoleParametersBase;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
            => context.CreateCollector(GameMatcher.Dead.Added());

        protected override bool Filter(GameEntity entity) =>
            entity.IsPlayer && entity.IsDead && !entity.IsDestroyed && _game.PauseEntity.Pause.Value == false;

        protected override void Execute(IEnumerable<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.IsUseGravity = false;

                var targetVerticalSpeed = entity.IsInWater
                    ? _playerWaterInHoleParametersBase.StayInWaterVerticalSpeed
                    : _deathParametersBase.VerticalVelocityAfterDeath;
                
                entity.ReplaceVerticalSpeed(targetVerticalSpeed);
                entity.ReplaceForwardSpeed(new ForwardSpeed(0,0));
            }
        }
    }
}