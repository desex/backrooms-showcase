using System.Threading;
using Cysharp.Threading.Tasks;
using Ecs.Managers;
using Ecs.Utils.ItemCollectables;
using UnityEngine;

namespace Game.Services.CollectablesItem
{
    public interface ICollectableSpawnService
    {
        void Add(Vector3 position);
        UniTask SpawnCollectable(ECollectableType collectableType, Vector3 position, CancellationToken token);
        UniTask DespawnCollectable(Uid collectableUid, CancellationToken token);
    }
}