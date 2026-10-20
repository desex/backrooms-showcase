using Db.ChasePlayer;
using Ecs.Action.Commands;
using Ecs.Utils;
using Game.Services.Player;
using Game.Utils.Movement;
using Generated.Commands;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Game.Systems.QTE
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 910, nameof(EFeatures.Qte))]
    public class FinishQteSystem : ForEachCommandUpdateSystem<FinishQteCommand>
    {
        private const int MAX_HIDE_CHECK_DEEP = 3;
        
        private readonly GameContext _game;
        private readonly ICommandBuffer _commandBuffer;
        private readonly IChasePlayerParameters _chasePlayerParameters;
        private readonly IPlayerMovementTypeService _playerMovementTypeService;

        public FinishQteSystem(
            GameContext game,
            ICommandBuffer commandBuffer,
            IChasePlayerParameters chasePlayerParameters,
            IPlayerMovementTypeService playerMovementTypeService
        ) : base(commandBuffer)
        {
            _game = game;
            _commandBuffer = commandBuffer;
            _chasePlayerParameters = chasePlayerParameters;
            _playerMovementTypeService = playerMovementTypeService;
        }

        protected override void Execute(ref FinishQteCommand command)
        {
            // Более показать не могу
        }

        private bool IsEntityHided(GameEntity entity)
        {
            // Более показать не могу
            return default;
        }
    }
}