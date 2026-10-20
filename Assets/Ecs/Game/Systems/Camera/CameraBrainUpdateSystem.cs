using Ecs.Core.Interfaces;
using Ecs.Utils;
using Game.Models.Camera;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;

namespace Ecs.Game.Systems.Camera
{
    [Install(ExecutionType.Game, ExecutionPriority.Urgent, 0, nameof(EFeatures.Camera))]
    public class CameraBrainUpdateSystem : ILateSystem
    {
        private readonly GameContext _game;
        private readonly IPlayerCameraHolder _playerCameraHolder;

        public CameraBrainUpdateSystem(IPlayerCameraHolder playerCameraHolder, GameContext game)
        {
            _playerCameraHolder = playerCameraHolder;
            _game = game;
        }

        public void Late()
        {
            // Более показать не могу
        }
    }
}