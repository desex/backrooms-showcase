using System;
using Db.Player;
using Ecs.Game.Components.Movement;
using Ecs.Utils;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;

namespace Ecs.Game.Systems.Movement
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 949, nameof(EFeatures.Move))]
    public class SpeedProgressionSystem
        : ComponentReplacedReactiveSystem<GameEntity, PassedDistanceInMetersComponent, int>
    {
        private readonly IPlayerSpeedProgressionParameters _playerSpeedProgressionParameters;
#if !DISABLE_GHOSTS
        private readonly IGameGroupUtils _gameGroupUtils;
#endif

        public SpeedProgressionSystem(
            IPlayerSpeedProgressionParameters playerSpeedProgressionParameters,
#if !DISABLE_GHOSTS
            IGameGroupUtils gameGroupUtils,
#endif
            GameContext game
        ) : base(game)
        {
            _playerSpeedProgressionParameters = playerSpeedProgressionParameters;
#if !DISABLE_GHOSTS
            _gameGroupUtils = gameGroupUtils;
#endif
        }

        protected override Type[] GetComponentTypes()
        {
            return GameComponentsLookup.ComponentTypes;
        }

        protected override int GetValue(PassedDistanceInMetersComponent component)
        {
            return component?.Value ?? 0;
        }

        protected override void OnReplaced(GameEntity entity, int last, int next)
        {
            if (entity.ForwardSpeedMultiplier.Value >= _playerSpeedProgressionParameters.MaxProgressionMultiply
                || next == last)
                return;

            IncreaseDistanceMultiplier(entity);

#if !DISABLE_GHOSTS
            using var _ = _gameGroupUtils.GetGhosts(out var ghosts);
            foreach (var ghost in ghosts)
                IncreaseDistanceMultiplier(ghost);
#endif
        }

        private void IncreaseDistanceMultiplier(GameEntity entity)
        {
            var newSpeedMultiplier = entity.ForwardSpeedMultiplier.Value
                                     + _playerSpeedProgressionParameters.ProgressionMultiply
                                     / _playerSpeedProgressionParameters.MetersProgression;

            entity.ReplaceForwardSpeedMultiplier(newSpeedMultiplier);
        }
    }
}