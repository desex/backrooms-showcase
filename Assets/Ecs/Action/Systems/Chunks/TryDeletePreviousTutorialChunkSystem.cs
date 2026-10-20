using System.Threading;
using Cysharp.Threading.Tasks;
using Ecs.Action.Commands.Chunks;
using Ecs.Utils;
using Ecs.Utils.Groups;
using Game.Services.Chunks;
using Game.Services.Generation;
using Game.Utils.Chunks;
using Game.Utils.Tutorial;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems.Chunks
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 5, nameof(EFeatures.Chunk))]
    public class TryDeletePreviousTutorialChunkSystem
        : ForEachCommandUpdateSystem<TryDeletePreviousTutorialChunkCommand>
    {
        private readonly IChunkService _chunkService;
        private readonly GameContext _game;
        private readonly IGameGroupUtils _gameGroupUtils;
        private readonly IGenerationTasksHandler _generationTasksHandler;

        public TryDeletePreviousTutorialChunkSystem(
            ICommandBuffer commandBuffer,
            GameContext game,
            IChunkService chunkService,
            IGameGroupUtils gameGroupUtils,
            IGenerationTasksHandler generationTasksHandler
        ) : base(commandBuffer)
        {
            _game = game;
            _chunkService = chunkService;
            _gameGroupUtils = gameGroupUtils;
            _generationTasksHandler = generationTasksHandler;
        }

        protected override void Execute(ref TryDeletePreviousTutorialChunkCommand command)
        {
            _generationTasksHandler.AddTaskToQueue(TryDeletePreviousTutorialChunkAsync);
        }

        private async UniTask TryDeletePreviousTutorialChunkAsync(CancellationToken token)
        {
            var tutorial = _game.TutorialEntity;

            var currentState = tutorial.TutorialState.Value;
            var isInTutorial = tutorial.IsInTutorial;

            if (!isInTutorial || currentState == ETutorialState.None)
                return;

            if (!TryGetPreviousStateLastChunk(currentState, out var previousStateLastChunk))
                return;
            
            var previousStateLastChunkNode = previousStateLastChunk.ChunkNode.Value;
            
            if (previousStateLastChunkNode.PreviousNode == null)
                return;
         
            var farthestChunkNode = ChunkNodeUtility.GetHead(previousStateLastChunkNode);
            
            CutConnections(previousStateLastChunkNode.PreviousNode, previousStateLastChunkNode);
            
            await _chunkService.DeactivateChunk(farthestChunkNode, token);
        }

        private bool TryGetPreviousStateLastChunk(ETutorialState currentState, out GameEntity previousStateLastChunk)
        {
            previousStateLastChunk = null;
            using var _ = _gameGroupUtils.GetChunks(out var chunks, entity => entity.HasTutorialChunkData);
            foreach (var chunk in chunks)
            {
                if (chunk.TutorialChunkData.Value.State == currentState)
                    continue;
                    
                if (chunk.ChunkNode.Value.NextNodes[0] == null)
                    continue;
                    
                var nextChunk = _game.GetEntityWithUid(chunk.ChunkNode.Value.NextNodes[0].ChunkUid);

                if (!IsState(nextChunk, currentState))
                    continue;
                
                previousStateLastChunk = chunk;
                break;
            }

            return previousStateLastChunk != null;
        }

        private static bool IsState(GameEntity entity, ETutorialState state)
        {
            if (!entity.HasTutorialChunkData)
                return false;

            var chunkData = entity.TutorialChunkData.Value;

            return chunkData.State == state;
        }
        
        private static void CutConnections(ChunkNode prevPreviousNode, ChunkNode previousNode)
        {
            if (prevPreviousNode.NextNodes[0] == previousNode)
                prevPreviousNode.NextNodes[0] = null;
            else if (prevPreviousNode.NextNodes[1] == previousNode)
                prevPreviousNode.NextNodes[1] = null;

            previousNode.PreviousNode = null;
        }
    }
}