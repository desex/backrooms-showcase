#if !DISABLE_GHOSTS
using Db.Ghost;
using Ecs.Utils;
using Ecs.Utils.Groups;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using UnityEngine;

namespace Ecs.Game.Systems.Ghosts
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 850, nameof(EFeatures.Move))]
    public class GhostFarAheadSystem : APausableGameStateConditionUpdateSystem
    {
        private readonly IGhostsParameters _ghostsParameters;
        private readonly IGameGroupUtils _gameGroupUtils;
        private readonly GameContext _game;

        public GhostFarAheadSystem(
            IGhostsParameters ghostsParameters,
            IGameGroupUtils gameGroupUtils,
            GameContext game
        ) : base(game)
        {
            _ghostsParameters = ghostsParameters;
            _gameGroupUtils = gameGroupUtils;
            _game = game;
        }

        protected override bool GameStateCondition(EGameState gameState) => gameState is EGameState.Game;

        protected override void UpdateSystem()
        {
            var player = _game.PlayerEntity;
            var playerPosition = player.Position.Value;

            using var _ = _gameGroupUtils.GetGhosts(out var ghosts);

            foreach (var ghost in ghosts)
            {
                var ghostPosition = ghost.Position.Value;
                var vectorToPlayer = ghostPosition - playerPosition;
                var sqrDistance = vectorToPlayer.sqrMagnitude;

                var thresholdDistance = _ghostsParameters.FailChancesDistance;

                var fullSqrThresholdDistance = thresholdDistance;
                fullSqrThresholdDistance *= fullSqrThresholdDistance;

                var isThresholdPassed = sqrDistance > fullSqrThresholdDistance;

                if (!isThresholdPassed)
                {
                    ghost.IsGhostFarAhead = false;
                    continue;
                }

                var ghostForward = ghost.Transform.Value.forward;
                var ghostDot = Vector3.Dot(ghostForward, vectorToPlayer.normalized);

                ghost.IsGhostFarAhead = ghostDot > 0;
            }
        }
    }
}
#endif