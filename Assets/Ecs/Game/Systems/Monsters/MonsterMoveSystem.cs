using Db.Monsters;
using DG.Tweening.Core.Easing;
using Ecs.Core.Interfaces;
using Ecs.Core.Systems;
using Ecs.Utils;
using Ecs.Utils.Monsters;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using UnityEngine;

namespace Ecs.Game.Systems.Monsters
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 850, nameof(EFeatures.Move))]
    public class MonsterMoveSystem : APausableConditionUpdateSystem
    {
        private readonly IChaserPresentationParameters _chaserPresentationParameters;
        private readonly ITimeProvider _timeProvider;

        public MonsterMoveSystem(
            IChaserPresentationParameters chaserPresentationParameters,
            ITimeProvider timeProvider,
            GameContext game
        ) : base(game)
        {
            _chaserPresentationParameters = chaserPresentationParameters;
            _timeProvider = timeProvider;
        }

        protected override bool Filter()
        {
            // Более показать не могу
            return default;
        }

        protected override void UpdateSystem()
        {
            // Более показать не могу
        }

        private void FollowPlayer()
        {
            // Более показать не могу
        }

        private void MoveToPlayer()
        {
            // Более показать не могу
        }

        private Vector3 GetPosition(GameEntity monster, GameEntity player)
        {
            // Более показать не могу
            return default;
        }

        private float GetDistance(GameEntity monster)
        {
            // Более показать не могу
            return default;
        }
    }
}