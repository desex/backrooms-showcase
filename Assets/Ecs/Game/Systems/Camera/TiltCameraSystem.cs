using System.Collections.Generic;
using Ecs.Utils;
using Game.Models.Camera;
using Game.Services.Camera;
using Game.Utils.Movement;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;

namespace Ecs.Game.Systems.Camera
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 1012, nameof(EFeatures.Camera))]
    public class TiltCameraSystem : ReactiveSystem<GameEntity>
    {
        private readonly ICameraService _cameraService;
        private readonly GameContext _game;

        public TiltCameraSystem(ICameraService cameraService, GameContext game) : base(game)
        {
            _cameraService = cameraService;
            _game = game;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            // Более показать не могу
            return default;
        }

        protected override bool Filter(GameEntity entity)
        {
            // Более показать не могу
            return default;
        }

        protected override void Execute(IEnumerable<GameEntity> entities)
        {
            // Более показать не могу
        }
    }
}