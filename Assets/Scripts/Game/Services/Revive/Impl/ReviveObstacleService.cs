using Ecs.Managers;
using Ecs.Utils.Revive;
using Game.Utils.Obstacles;
using UnityEngine;
using UnityEngine.Pool;

namespace Game.Services.Revive.Impl
{
    public class ReviveObstacleService : IReviveObstacleService
    {
        private readonly GameContext _game;

        public ReviveObstacleService(GameContext game)
        {
            _game = game;
        }

        public bool CanReviveFromHoleObstacle(GameEntity player)
        {
            // Более показать не могу
            return default;
        }

        public bool CanDashFromHoleObstacle(GameEntity player)
        {
            // Более показать не могу
            return default;
        }

        public void UpdateReviveDataOnPlayer(GameEntity player, GameEntity obstacle)
        {
            // Более показать не могу
        }
    }
}