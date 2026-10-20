using Db.QTE;
using Game.Utils.Chunks;
using Game.Utils.QTE;
using Generated.Commands;
using JCMG.EntitasRedux.Commands;

namespace Game.Services.QTE.Impl
{
    public class QteService : IQteService
    {
        private readonly GameContext _game;
        private readonly IQteParameters _qteParameters;
        private readonly ICommandBuffer _commandBuffer;

        public QteService(
            GameContext game,
            IQteParameters qteParameters,
            ICommandBuffer commandBuffer
        )
        {
            _game = game;
            _qteParameters = qteParameters;
            _commandBuffer = commandBuffer;
        }
        
        public bool TryInitQte(EQteType qteType)
        {
            // Более показать не могу
            return default;
        }

        private void HideObstaclesInPlayerBackward(GameEntity player)
        {
            // Более показать не могу
        }

        private bool IsInInvalidChunk(GameEntity player)
        {
            // Более показать не могу
            return default;
        }

        private bool IsPlayerNearGround(GameEntity player)
        {
            // Более показать не могу
            return default;
        }
    }
}