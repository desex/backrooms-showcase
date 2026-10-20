#if !DISABLE_GHOSTS
using Db.Ghost;
using Db.SoundFxBase;
using Ecs.Utils;
using Ecs.Utils.Groups;
using Game.Services.Ghosts.Sound;
using Generated.Commands;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Game.Systems.Ghosts
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 850, nameof(EFeatures.Move))]
    public class KillFarGhostSystem : APausableGameStateConditionUpdateSystem
    {
        private readonly GameContext _game;
        private readonly IGameGroupUtils _gameGroupUtils;
        private readonly IGhostsParameters _ghostsParameters;
        private readonly ICommandBuffer _commandBuffer;
        private readonly IGhostSoundDispatcher _ghostSoundDispatcher;

        public KillFarGhostSystem(
            GameContext game, 
            IGameGroupUtils gameGroupUtils,
            IGhostsParameters ghostsParameters,
            ICommandBuffer commandBuffer,
            IGhostSoundDispatcher ghostSoundDispatcher
        ) : base(game)
        {
            _game = game;
            _gameGroupUtils = gameGroupUtils;
            _ghostsParameters = ghostsParameters;
            _commandBuffer = commandBuffer;
            _ghostSoundDispatcher = ghostSoundDispatcher;
        }

        protected override bool GameStateCondition(EGameState gameState) => gameState is EGameState.Game;

        protected override void UpdateSystem()
        {
            var character = _game.PlayerEntity;
            var characterPosition = character.Position.Value;

            using var _ = _gameGroupUtils.GetGhosts(out var ghosts, entity => !entity.IsDead && entity.IsActive);

            var killGhostSqrDistance = _ghostsParameters.KillGhostDistance * _ghostsParameters.KillGhostDistance;
            foreach (var entity in ghosts)
            {
                var ghostPosition = entity.Position.Value;
                var sqrDistance = (characterPosition - ghostPosition).sqrMagnitude;
                if (sqrDistance < killGhostSqrDistance)
                    continue;

                PlayGhostDeathSound(entity);
                _commandBuffer.KillPlayer(entity.Uid.Value);
            }

            return;
            
            void PlayGhostDeathSound(GameEntity entity)
            {
                var entityUid = entity.Uid.Value;
                _ghostSoundDispatcher.PlaySound(entityUid, ESoundFxName.GhostDeath);
            }
        }
    }
}
#endif