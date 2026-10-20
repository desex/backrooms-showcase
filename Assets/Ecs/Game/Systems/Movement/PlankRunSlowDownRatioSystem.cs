using Db.Player;
using Ecs.Core.Interfaces;
using Ecs.Utils;
using Ecs.Utils.Groups;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using UnityEngine;

namespace Ecs.Game.Systems.Movement
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 666, nameof(EFeatures.Move))]
    public class PlankRunSlowDownRatioSystem : APausableCharacterGameStateConditionUpdateSystem
    {
        private readonly IPlayerPlankRunParametersBase _playerPlankRunParametersBase;
        private readonly ITimeProvider _timeProvider;

        public PlankRunSlowDownRatioSystem(
            GameContext game,
            IGameGroupUtils gameGroupUtils,
            IPlayerPlankRunParametersBase playerPlankRunParametersBase,
            ITimeProvider timeProvider
        ) : base(game, gameGroupUtils)
        {
            _playerPlankRunParametersBase = playerPlankRunParametersBase;
            _timeProvider = timeProvider;
        }

        protected override bool GameStateCondition(EGameState gameState)
        {
            return gameState == EGameState.Game;
        }

        protected override bool EntityFilter(GameEntity entity)
        {
            return (entity.IsNeedSlowDown || entity.HasSlowDownRatio)
                   && entity.IsDead == false
                   && entity.IsDestroyed == false
                   && entity.IsDamaged == false;
        }

        protected override void HandleUpdate(GameEntity entity)
        {
            var isNeedSlowDown = entity.IsNeedSlowDown;

            var speed = isNeedSlowDown
                ? _playerPlankRunParametersBase.SlowDownRatioIncreaseSpeed
                : -_playerPlankRunParametersBase.SlowDownRatioDecreaseSpeed;

            var maxSlowDown = _playerPlankRunParametersBase.MaxSlowDownRatio;
            var deltaRatio = speed * _timeProvider.DeltaTime;

            if (isNeedSlowDown && entity.HasSlowDownRatio == false)
            {
                entity.ReplaceSlowDownRatio(deltaRatio);
                return;
            }

            var nextSlowDownRatio = entity.SlowDownRatio.Value + deltaRatio;
            nextSlowDownRatio = Mathf.Clamp(nextSlowDownRatio, 0f, maxSlowDown);

            if (isNeedSlowDown == false && nextSlowDownRatio <= 0)
            {
                entity.RemoveSlowDownRatio();
                return;
            }

            entity.ReplaceSlowDownRatio(nextSlowDownRatio);
        }
    }
}