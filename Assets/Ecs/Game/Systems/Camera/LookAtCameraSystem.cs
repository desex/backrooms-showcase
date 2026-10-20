using System;
using Ecs.Game.Components.Movement;
using Ecs.Utils;
using Game.Models.Camera;
using Game.Services.Camera;
using Game.Utils.Movement;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;

namespace Ecs.Game.Systems.Camera
{
    [Install(ExecutionType.Game, ExecutionPriority.High, 10, nameof(EFeatures.Camera))]
    public class LookAtCameraSystem : ComponentReplacedReactiveSystem<GameEntity, BodyMoveTypeComponent, EBodyMoveType>
    {
        private readonly ICameraService _cameraService;
        private readonly GameContext _game;

        public LookAtCameraSystem(
            ICameraService cameraService,
            GameContext game
        ) : base(game)
        {
            _cameraService = cameraService;
            _game = game;
        }

        protected override Type[] GetComponentTypes()
        {
            // Более показать не могу
            return global::System.Array.Empty<Type>();
        }

        protected override EBodyMoveType GetValue(BodyMoveTypeComponent component)
        {
            // Более показать не могу
            return default;
        }

        protected override void OnReplaced(GameEntity entity, EBodyMoveType last, EBodyMoveType next)
        {
            // Более показать не могу
        }
    }
}