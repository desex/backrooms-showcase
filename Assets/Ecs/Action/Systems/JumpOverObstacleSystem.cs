using Db.Player;
using Db.SoundFxBase;
using Ecs.Action.Commands;
using Ecs.Utils;
using Game.Services.Player;
using Game.Services.Sound;
using Game.Utils.Movement;
using Generated.Commands;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;
using UnityEngine;

namespace Ecs.Action.Systems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 591, nameof(EFeatures.Move))]
    public class JumpOverObstacleSystem : ForEachCommandUpdateSystem<JumpOverObstacleCommand>
    {
        private readonly ICommandBuffer _commandBuffer;
        private readonly GameContext _game;
        private readonly IPlayerJumpOverObstacleParametersBase _playerJumpOverObstacleParameters;
        private readonly IPlayerMovementTypeService _playerMovementTypeService;
        private readonly ISoundService _soundService;

        public JumpOverObstacleSystem(
            ICommandBuffer commandBuffer,
            GameContext game,
            ISoundService soundService,
            IPlayerMovementTypeService playerMovementTypeService,
            IPlayerJumpOverObstacleParametersBase playerJumpOverObstacleParameters
        ) : base(commandBuffer)
        {
            _commandBuffer = commandBuffer;
            _game = game;
            _soundService = soundService;
            _playerMovementTypeService = playerMovementTypeService;
            _playerJumpOverObstacleParameters = playerJumpOverObstacleParameters;
        }

        protected override void Execute(ref JumpOverObstacleCommand command)
        {
            // Более показать не могу
        }
    }
}