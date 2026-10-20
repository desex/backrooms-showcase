using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Db.GameLevelsSettings.Impls.Sequence;
using Ecs.Managers;
using JetBrains.Annotations;

namespace Game.Utils.Chunks
{
    public sealed class ChunkNode
    {
        public EChunkType ChunkType;
        public Uid ChunkUid;

        public bool IsGenerationStopped = false;

        public int OrderInSequence;
        public ChunkNode PreviousNode;

        [CanBeNull]
        public SequenceParameters SequenceParameters;

        public ChunkNode(Uid uid, EChunkType chunkType)
        {
            ChunkUid = uid;
            ChunkType = chunkType;
            OrderInSequence = 1;
        }

        public ChunkNode(Uid uid, EChunkType chunkType, GameEntity previousChunk)
        {
            if (previousChunk is null)
                throw new NullReferenceException("Provided chunk node is null.");

            ChunkUid = uid;
            ChunkType = chunkType;
            PreviousNode = previousChunk.ChunkNode.Value;

            if (PreviousNode.ChunkType == ChunkType)
                OrderInSequence = PreviousNode.OrderInSequence + 1;
        }

        public ChunkNode[] NextNodes { get; } = new ChunkNode[2];

        public async UniTask MakeActionWithEveryEndNode(
            Func<ChunkNode, CancellationToken, UniTask> nodeAction,
            CancellationToken token
        )
        {
            var isEndNode = true;

            foreach (var nextNode in NextNodes)
            {
                if (nextNode is not null)
                {
                    isEndNode = false;
                    await nextNode.MakeActionWithEveryEndNode(nodeAction, token);
                }
            }

            if (isEndNode)
                await nodeAction!.Invoke(this, token);
        }
    }
}