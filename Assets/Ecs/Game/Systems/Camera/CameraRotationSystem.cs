using Db.Camera;
using DG.Tweening;
using Ecs.Core.Interfaces;
using Ecs.Core.Systems;
using Ecs.Utils;
using Game.Utils.Movement;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using UnityEngine;

namespace Ecs.Game.Systems.Camera
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 1012, nameof(EFeatures.Camera))]
    public class CameraRotationSystem : APausableConditionUpdateSystem
    {
        private readonly ICameraParameters _cameraParameters;
        private readonly ITimeProvider _timeProvider;
        private readonly GameContext _game;

        public CameraRotationSystem(
            ICameraParameters cameraParameters,
            ITimeProvider timeProvider,
            GameContext game  
        ) : base(game)
        {
            _cameraParameters = cameraParameters;
            _timeProvider = timeProvider;
            _game = game;
        }

        protected override void UpdateSystem()
        {
            // Более показать не могу
        }

        private static bool IsCameraRotationBlocked(GameEntity player, GameEntity chaser)
        {
            // Более показать не могу
            return default;
        }
    }
}