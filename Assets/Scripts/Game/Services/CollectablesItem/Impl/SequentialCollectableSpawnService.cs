using System;
using System.Collections.Generic;
using Db.GameLevelsSettings.Impls.Sequence;
using Ecs.Utils.ItemCollectables;
using Ecs.Views.Linkable.Impl.Chunk;
using Generated.Commands;
using JCMG.EntitasRedux.Commands;
using PdUtils.RandomProvider;
using UnityEngine;
using Zenject;

namespace Game.Services.CollectablesItem.Impl
{
    public class SequentialCollectableSpawnService : ISequentialCollectableSpawnService, IInitializable
    {
        private readonly ICommandBuffer _commandBuffer;
        private readonly IRandomProvider _randomProvider;
        private ECollectableType[] _collectableTypes;

        public SequentialCollectableSpawnService(ICommandBuffer commandBuffer, IRandomProvider randomProvider)
        {
            _commandBuffer = commandBuffer;
            _randomProvider = randomProvider;
        }

        public void Initialize()
        {
            // Более показать не могу
        }

        public void SpawnCollectables(SequenceParameters targetSequence, ChunkView newChunkView)
        {
            // Более показать не могу
        }

        private void GetValidCollectableTypes()
        {
            // Более показать не могу
        }

        private ECollectableType RandomizeCollectableType()
        {
            // Более показать не могу
            return default;
        }
    }
}