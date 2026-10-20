using Cysharp.Threading.Tasks;
using Db.Player;
using Ecs.Action.Commands.Movement;
using Ecs.Utils;
using Game.Utils.UniTaskExtensions;
using Generated.Commands;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems.Movement
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 963, nameof(EFeatures.Move))]
    public class LandingSystem : ForEachCommandUpdateSystem<LandingCommand>
    {
        private readonly ICommandBuffer _commandBuffer;
        private readonly GameContext _game;
        private readonly IPlayerSlideAndJumpParameters _playerSlideAndJumpParameters;

        public LandingSystem(
            ICommandBuffer commandBuffer,
            GameContext game,
            IPlayerSlideAndJumpParameters playerSlideAndJumpParameters
        ) : base(commandBuffer)
        {
            _commandBuffer = commandBuffer;
            _game = game;
            _playerSlideAndJumpParameters = playerSlideAndJumpParameters;
        }

        protected override void Execute(ref LandingCommand command)
        {
            var player = _game.PlayerEntity;

            player.IsReadyToFailFall = false;
            player.IsCanSwipe = false;

            var landDuration = _playerSlideAndJumpParameters.LandDuration;
            CancelLandAfterDelay(landDuration, player).ForgetAndWrapExceptions();
        }

        private async UniTask CancelLandAfterDelay(float delay, GameEntity player)
        {
            await UniTaskUtils.PausableDelay(delay, () => _game.Pause.Value);
            CancelLand(player);
        }

        private void CancelLand(GameEntity player)
        {
            player.IsCanSwipe = true;

            if (!player.HasReadyToObstaclePass)
              _commandBuffer.StartSlide(player.Uid.Value);

            if (!player.HasObstacleActivationOnLanding)
                return;

            var passObstacleType = player.ObstacleActivationOnLanding.Value;
            
            _commandBuffer.TryDashWhenInObstacle(player.Uid.Value, passObstacleType);
            player.RemoveObstacleActivationOnLanding();
        }
    }
}