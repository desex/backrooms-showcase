using Db.Player;
using Ecs.Action.Commands;
using Ecs.Utils;
using Ecs.Utils.Movement;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;
using UnityEngine;

namespace Ecs.Action.Systems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 940, nameof(EFeatures.Move))]
    public class MoveVerticalSystem : ForEachCommandUpdateSystem<MoveVerticalCommand>
    {
        private readonly GameContext _game;
        private readonly IPlayerInputParametersBase _playerInputParametersBase;

        public MoveVerticalSystem(
            ICommandBuffer commandBuffer,
            GameContext game,
            IPlayerInputParametersBase playerInputParametersBase
        ) : base(commandBuffer)
        {
            _game = game;
            _playerInputParametersBase = playerInputParametersBase;
        }

        protected override void Execute(ref MoveVerticalCommand command)
        {
            var player = _game.PlayerEntity;
            var delta = command.NormalizedDelta;

            if (Mathf.Abs(delta) < _playerInputParametersBase.JoystickNormalizedDeadZone)
            {
                player.ReplaceVerticalMoveDirection(EVerticalMoveDirection.None);
                return;
            }

            var direction = delta > 0 ? EVerticalMoveDirection.Up : EVerticalMoveDirection.Down;

            player.ReplaceVerticalMoveDirection(direction);
        }
    }
}