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
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 590, nameof(EFeatures.Move))]
    public class StartJumpOnSystem : ForEachCommandUpdateSystem<JumpOnCommand>
    {
        private readonly ICommandBuffer _commandBuffer;
        private readonly GameContext _game;
        private readonly IPlayerMovementTypeService _playerMovementTypeService;
        private readonly ISoundService _soundService;

        public StartJumpOnSystem(
            ICommandBuffer commandBuffer,
            GameContext game,
            IPlayerMovementTypeService playerMovementTypeService,
            ISoundService soundService
        ) : base(commandBuffer)
        {
            _game = game;
            _playerMovementTypeService = playerMovementTypeService;
            _soundService = soundService;
            _commandBuffer = commandBuffer;
        }

        protected override void Execute(ref JumpOnCommand command)
        {
            // Более показать не могу
        }
    }
}