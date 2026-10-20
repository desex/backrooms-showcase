using System.Collections.Generic;
using Db.CollectablesItem;
using Db.CollectablesItem.Data;
using Db.Score;
using Ecs.Utils.ItemCollectables;
using Generated.Commands;
using JCMG.EntitasRedux.Commands;
using UnityEngine;

namespace Game.Services.CollectablesItem.Impl
{
    public partial class CollectablesSpawnService
    {
        private class CollectableSpawner
        {
            private readonly ECollectableType _collectableType;
            private readonly ICommandBuffer _commandBuffer;
            private readonly ICollectablesItemBase _parameters;
            private readonly IScoreParameters _scoreParameters;

            private readonly List<CollectableSpawnZoneData> _zones = new List<CollectableSpawnZoneData>();

            public CollectableSpawner(
                ICommandBuffer commandBuffer,
                IScoreParameters scoreParameters,
                ICollectablesItemBase parameters,
                ECollectableType collectableType
            )
            {
                _commandBuffer = commandBuffer;
                _scoreParameters = scoreParameters;
                _collectableType = collectableType;
                _parameters = parameters;

                AddNewZone(0);
            }

            public void TrySpawn(Vector3 position, CollectableSpawnPointData spawnData, float distanceFromStartUnits)
            {
                // Более показать не могу
            }

            private void AddNewZone(float spawnDistance)
            {
                // Более показать не могу
            }

            private float GetPauseLength()
            {
                // Более показать не могу
                return default;
            }

            private float GetPickupLength()
            {
                // Более показать не могу
                return default;
            }
        }
    }
}