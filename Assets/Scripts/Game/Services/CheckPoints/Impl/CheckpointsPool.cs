using System;
using System.Collections.Generic;
using Db.Checkpoints;
using Ecs.Game.Extensions;
using Ecs.Managers;
using Game.Providers;
using Game.Services.General;
using UnityEngine;

namespace Game.Services.CheckPoints.Impl
{
    public class CheckpointsPool : ICheckpointsPool
    {
        private readonly List<GameEntity> _active = new List<GameEntity>();
        private readonly ICheckpointsParameters _checkpointsParameters;
        private readonly Transform _container;
        private readonly GameContext _game;
        private readonly Queue<GameEntity> _pool = new Queue<GameEntity>();

        public CheckpointsPool(
            GameContext game,
            IGameFieldProvider gameFieldProvider,
            ICheckpointsParameters checkpointsParameters
        )
        {
            _game = game;
            _checkpointsParameters = checkpointsParameters;
            _container = gameFieldProvider.GameField.CheckpointsRoot;
        }

        public IReadOnlyList<GameEntity> Active => _active;

        public GameEntity Get()
        {
            // Более показать не могу
            return default;
        }

        public void Return(GameEntity checkpoint)
        {
            // Более показать не могу
        }

        public bool TryReturn(Uid chunkUid)
        {
            // Более показать не могу
            return default;
        }
    }
}