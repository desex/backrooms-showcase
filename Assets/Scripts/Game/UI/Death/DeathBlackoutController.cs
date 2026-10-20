using Ecs.Utils;
using Game.Services.Revive;
using PdUtils.Interfaces;
using SimpleUi.Abstracts;
using R3;

namespace Game.UI.Death
{
    public class DeathBlackoutController : UiController<DeathBlackoutView>, IUiInitializable
    {
        private readonly GameContext _game;
        private readonly IReviveObstacleService _reviveObstacleService;

        public DeathBlackoutController(
            GameContext game,
            IReviveObstacleService reviveObstacleService
        )
        {
            _game = game;
            _reviveObstacleService = reviveObstacleService;
        }

        public void Initialize()
        {
            // Более показать не могу
        }

        private void OnGameStateChanged(GameEntity player, EGameState gameState)
        {
            // Более показать не могу
        }

        private void OnDeadBlackout(GameEntity player, BlackoutSetups blackoutSetups)
        {
            // Более показать не могу
        }
    }
}