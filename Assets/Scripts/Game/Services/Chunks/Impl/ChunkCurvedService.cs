using Db.CurvedWorld;
using Ecs.Managers;
using Game.Services.CurvedWorld;

namespace Game.Services.Chunks.Impl
{
    public class ChunkCurvedService : IChunkCurvedService
    {
        private readonly ICurvedWorldService _curvedWorldService;
        private readonly ICurvedWorldTypeParameters _curvedWorldTypeParameters;
        private readonly GameContext _game;
        private int _eventDurationInCountSequences;
        private int _numberСompletedSequences;
        
        public ChunkCurvedService(
            ICurvedWorldService curvedWorldService, 
            GameContext game, 
            ICurvedWorldTypeParameters curvedWorldTypeParameters
        )
        {
            _curvedWorldService = curvedWorldService;
            _game = game;
            _curvedWorldTypeParameters = curvedWorldTypeParameters;
        }
        
        public void SetBendSettings(Uid chunkId)
        {
            var player = _game.PlayerEntity;
            
            if (player.CountCurvedWorldEventInLevel.Value >= _curvedWorldTypeParameters.CountCurvedWorldEventInLevel)
                return;
            
            if (player.IsActiveCurvedWorldEvent)
            {
                player.ReplaceSequenceCompletion(chunkId);
                return;
            }
            
            var chunkEntity = _game.GetEntityWithUid(chunkId);
            
            if (!chunkEntity.IsSequential)
                return;
            
            var currentSequence = chunkEntity.ChunkNode.Value.SequenceParameters;
            
            var eventDurationInCountSequences = _curvedWorldService.TrySetBendSettings(out var isSuccessfully, currentSequence);
            
            if (!isSuccessfully)
                return;
            
            _eventDurationInCountSequences = eventDurationInCountSequences;
            _numberСompletedSequences = 0;
        }
        
        public void CompleteSequence(Uid chunkId)
        {
            var player = _game.PlayerEntity;
            var chunkEntity = _game.GetEntityWithUid(chunkId);
            
            if (chunkEntity.IsSequential)
            {
                _numberСompletedSequences++;
            }
            
            player.RemoveSequenceCompletion();
            
            if (_numberСompletedSequences == _eventDurationInCountSequences)
            {
                ResetCurvedWorld();
            }
        }
        
        public void ResetCurvedWorld()
        {
            _curvedWorldService.AddCountCurvedWorldEventInLevel(1);
            _game.PlayerEntity.IsActiveCurvedWorldEvent = false;
            _curvedWorldService.ResetCurvedWorld();
            _eventDurationInCountSequences = 0;
            _numberСompletedSequences = 0;
        }
    }
}