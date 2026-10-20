using System;
using Cysharp.Threading.Tasks;
using Db.Player;
using Ecs.Action.Commands;
using Ecs.Utils;
using Game.Utils.Movement;
using Game.Utils.UniTaskExtensions;
using Generated.Commands;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 615, nameof(EFeatures.Move))]
    public class SetReadyControllableSlideSystem : ForEachCommandUpdateSystem<SetReadyControllableSlideCommand>
    {
        private readonly ICommandBuffer _commandBuffer;
        private readonly GameContext _game;
        private readonly IPlayerSlideAndJumpParameters _playerSlideAndJumpParameters;

        public SetReadyControllableSlideSystem(
            ICommandBuffer commandBuffer,
            GameContext game,
            IPlayerSlideAndJumpParameters playerSlideAndJumpParameters
        ) : base(commandBuffer)
        {
            _commandBuffer = commandBuffer;
            _game = game;
            _playerSlideAndJumpParameters = playerSlideAndJumpParameters;
        }

        protected override void Execute(ref SetReadyControllableSlideCommand command)
        {
            var character = _game.GetEntityWithUid(command.CharacterUid);

            if (command.IsReady)
            {
                _commandBuffer.StartSlide(character.Uid.Value);
                character.ReplaceReadyForControllableSlide(command.StartSpeed);

                return;
            }

            if (character.HasReadyForControllableSlide)
                character.RemoveReadyForControllableSlide();

            character.IsControllableSlideInterrupted = false;

            EndSlidingAfterDelay(character).ForgetAndWrapExceptions();
        }

        private async UniTask EndSlidingAfterDelay(GameEntity player)
        {
            var delay = _playerSlideAndJumpParameters.TimeToEndControllableSliding;
            await UniTaskUtils.PausableDelay(delay, () => _game.Pause.Value);

            TryEndSliding(player);
        }

        private void TryEndSliding(GameEntity player)
        {
            if (player.HasReadyForControllableSlide)
                return;

            _commandBuffer.CancelSlide(player.Uid.Value);

            if (!player.HasForwardSpeed)
                return;

            var currentTargetForwardSpeed = player.ForwardSpeed.Value.Target;
            player.ReplaceForwardSpeed(new ForwardSpeed(currentTargetForwardSpeed, currentTargetForwardSpeed));
        }
    }
}