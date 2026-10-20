using System.Threading;
using Cysharp.Threading.Tasks;
using Db.GameLevelsSettings.Impls.Sequence;
using Ecs.Managers;
using Game.Utils.Chunks;
using UnityEngine;

namespace Game.Services.Chunks
{
    public interface IChunkGenerationService
    {
        Uid LastChunkUid { get; }

        void Initialize();
        UniTask GenerateFirstSequence(SequenceParameters parameters, Vector3 offset, CancellationToken token);
        UniTask GenerateNextChunk(ChunkNode node, CancellationToken token);
        void Reset();
    }
}