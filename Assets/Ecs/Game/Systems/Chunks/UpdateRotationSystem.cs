using Db.Player;
using Ecs.Core.Interfaces;
using Ecs.Core.Systems;
using Ecs.Utils;
using Ecs.Utils.Groups;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using UnityEngine;

namespace Ecs.Game.Systems.Chunks
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 945, nameof(EFeatures.Move))]
    public class UpdateRotationSystem : APausableCharacterConditionUpdateSystem
    {
        private readonly IPlayerBasicMovementParametersBase _playerBasicMovementParameters;
        private readonly ITimeProvider _timeProvider;

        public UpdateRotationSystem(
            GameContext game,
            IGameGroupUtils gameGroupUtils,
            ITimeProvider timeProvider,
            IPlayerBasicMovementParametersBase playerBasicMovementParameters
        ) : base(game, gameGroupUtils)
        {
            _timeProvider = timeProvider;
            _playerBasicMovementParameters = playerBasicMovementParameters;
        }

        protected override bool EntityFilter(GameEntity entity)
        {
            return entity.HasTargetRotation && entity.HasForwardSpeed;
        }

        protected override void HandleUpdate(GameEntity entity)
        {
            var toRotation = entity.TargetRotation.Value;
            var fromRotation = entity.Rotation.Value;
            var delta = _timeProvider.DeltaTime * _playerBasicMovementParameters.RotationSpeed;
            var targetRotation = Quaternion.Lerp(fromRotation, toRotation, delta);

            entity.ReplaceRotation(targetRotation);

            if (Quaternion.Angle(fromRotation, toRotation) > MathConst.EPSILON)
                return;

            entity.RemoveTargetRotation();
        }
    }
}