using System.Threading;
using Cysharp.Threading.Tasks;
using Db.Score;
using Ecs.Action.Commands.Chunks;
using Ecs.Managers;
using Ecs.Utils;
using Ecs.Utils.Groups;
using Game.Services.Chunks;
using Game.Services.DistanceCalculation;
using Game.Services.Generation;
using Game.Services.Providers;
using Game.Utils.Chunks;
using Game.Utils.DistanceCalculation;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems.Chunks
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 5, nameof(EFeatures.Chunk))]
    public sealed class DeletePrePreviousChunkSystem : ForEachCommandUpdateSystem<DeletePrePreviousChunkCommand>
    {
        private readonly GameContext _game;
        private readonly IChunkService _chunkService;
        private readonly IGenerationTasksHandler _generationTasksHandler;
        private readonly IDistanceCalculationService _distanceCalculationService;
        private readonly IChunkSegmentService _chunkSegmentService;
        private readonly IGameGroupUtils _gameGroupUtils;
        private readonly IScoreParameters _scoreParameters;
        private readonly IGenerationResourcesProvider _generationResourcesProvider;

        public DeletePrePreviousChunkSystem(
            GameContext game,
            ICommandBuffer commandBuffer,
            IChunkService chunkService,
            IGenerationTasksHandler generationTasksHandler,
            IDistanceCalculationService distanceCalculationService,
            IScoreParameters scoreParameters,
            IGenerationResourcesProvider generationResourcesProvider,
            IChunkSegmentService chunkSegmentService,
            IGameGroupUtils gameGroupUtils
        ) : base(commandBuffer)
        {
            _game = game;
            _chunkService = chunkService;
            _generationTasksHandler = generationTasksHandler;
            _distanceCalculationService = distanceCalculationService;
            _scoreParameters = scoreParameters;
            _generationResourcesProvider = generationResourcesProvider;
            _chunkSegmentService = chunkSegmentService;
            _gameGroupUtils = gameGroupUtils;
        }

        protected override void Execute(ref DeletePrePreviousChunkCommand command)
        {
            var currentChunkId = command.CurrentEnteredChunkId;

            _generationTasksHandler
                .AddTaskToQueue(async token => await DeletePrevPreviousChunkAsync(currentChunkId, token));
        }

        private async UniTask DeletePrevPreviousChunkAsync(Uid currentChunkUid, CancellationToken token)
        {
            var currentChunk = _game.GetEntityWithUid(currentChunkUid);
            var currentChunkNode = currentChunk.ChunkNode.Value;

            if (IsClose(currentChunkNode, out var farthestChunkNode))
                return;

            if (!IsPrevNodeExist(currentChunkNode, out var previousNode))
                return;

            if (IsPrevPrevNodeExist(previousNode, out var prevPreviousNode))
                return;
            
            await DeleteChunks(previousNode, prevPreviousNode, farthestChunkNode, token);
        }

        private bool IsClose(ChunkNode currentChunkNode, out ChunkNode farthestChunkNode)
        {
            farthestChunkNode = ChunkNodeUtility.GetHead(currentChunkNode);
            if (farthestChunkNode is null)
                return true;

            if (!IsAwayFromPlayer(farthestChunkNode))
                return true;

            return false;
        }

        private bool IsPrevNodeExist(ChunkNode currentChunkNode, out ChunkNode previousNode)
        {
            previousNode = GetPreviousNode(currentChunkNode, false);

            return previousNode != null;
        }

        private bool IsPrevPrevNodeExist(ChunkNode previousNode, out ChunkNode prevPreviousNode)
        {
            prevPreviousNode = GetPreviousNode(previousNode);

            if (prevPreviousNode == null)
                return true;

            var prevPreviousChunk = _game.GetEntityWithUid(prevPreviousNode.ChunkUid);

            if (prevPreviousChunk == null)
                return true;

            if (prevPreviousChunk.HasTutorialChunkData)
                return true;

            return false;
        }

        private bool IsAwayFromPlayer(ChunkNode farthestChunkNode)
        {
            var playerDistance = _distanceCalculationService.GetPlayerDistance(_chunkSegmentService.Segments);
            var farthestChunk = _game.GetEntityWithUid(farthestChunkNode.ChunkUid);

            var farthestChunkEndPosition = farthestChunk.ChunkAnchors.Value.EndOnlyAnchor.position;
            var farthestChunkEndDistance = _distanceCalculationService.GetDistance(farthestChunkEndPosition, _chunkSegmentService.Segments);

            var distance = (playerDistance - farthestChunkEndDistance) * _scoreParameters.DistanceToMetersRatio;
            
            return distance > _generationResourcesProvider.ChunkGenerationSettings.DistanceToFarthestChunk;
        }

        private async UniTask DeleteChunks(ChunkNode previousNode, ChunkNode prevPreviousNode, ChunkNode farthestNode, CancellationToken token)
        {
            await DeleteAllTutorialChunksAsync(token);

            CutConnections(prevPreviousNode, previousNode);
            
            await _chunkService.DeactivateChunk(farthestNode, token);
        }

        private ChunkNode GetPreviousNode(ChunkNode node, bool withNodeReferenceClearing = true)
        {
            var previousNode = node.PreviousNode;

            if (withNodeReferenceClearing)
                node.PreviousNode = null;

            return previousNode;
        }
        
        private async UniTask DeleteAllTutorialChunksAsync(CancellationToken token)
        {
            using var _ = _gameGroupUtils.GetChunks(out var chunks, entity => entity.HasTutorialChunkData);

            if (chunks.Count == 0)
                return;

            GameEntity lastTutorialChunk = null;
            GameEntity firstNormalChunk = null;
            foreach (var tutorialChunk in chunks)
            {
                if (tutorialChunk.ChunkNode.Value.NextNodes[0] == null)
                    continue;
                    
                var nextChunk = _game.GetEntityWithUid(tutorialChunk.ChunkNode.Value.NextNodes[0].ChunkUid);

                if (nextChunk.HasTutorialChunkData)
                    continue;

                firstNormalChunk = nextChunk;
                lastTutorialChunk = tutorialChunk;
                break;
            }
            
            if (lastTutorialChunk == null)
                return;
            
            var lastTutorialChunkNode = lastTutorialChunk.ChunkNode.Value;
            var firstNormalChunkNode = firstNormalChunk.ChunkNode.Value;
            var farthestChunkNode = ChunkNodeUtility.GetHead(lastTutorialChunkNode);
            
            CutConnections(lastTutorialChunkNode, firstNormalChunkNode);
            
            await _chunkService.DeactivateChunk(farthestChunkNode, token);
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