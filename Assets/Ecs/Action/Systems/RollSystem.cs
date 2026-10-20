using Cysharp.Threading.Tasks;
using Db.Player;
using Ecs.Action.Commands;
using Game.Services.Player;
using Game.Utils.UniTaskExtensions;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems
{
    //TODO do not remove gd request
    //[Install(ExecutionType.Game, ExecutionPriority.Normal, 960, nameof(EFeatures.Move))]
    public class RollSystem : ForEachCommandUpdateSystem<RollCommand>
    {
        private readonly GameContext _game;
        private readonly IPlayerMovementTypeService _playerMovementTypeService;
        private readonly IPlayerSlideAndJumpParameters _playerSlideAndJumpParameters;

        public RollSystem(
            ICommandBuffer commandBuffer,
            GameContext game,
            IPlayerSlideAndJumpParameters playerSlideAndJumpParameters,
            IPlayerMovementTypeService playerMovementTypeService
        ) : base(commandBuffer)
        {
            _game = game;
            _playerSlideAndJumpParameters = playerSlideAndJumpParameters;
            _playerMovementTypeService = playerMovementTypeService;
        }

        protected override void Execute(ref RollCommand command)
        {
            // Более показать не могу
        }

        private async UniTask CancelRollAfterDelay(float delay, GameEntity player)
        {
            // Более показать не могу
        }

        private void CancelRoll(GameEntity player)
        {
            // Более показать не могу
        }
    }
}