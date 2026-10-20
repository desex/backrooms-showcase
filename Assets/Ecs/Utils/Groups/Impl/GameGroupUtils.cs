using System;
using System.Collections.Generic;
using JCMG.EntitasRedux;
using Unity.Collections;
using UnityEngine.Pool;

namespace Ecs.Utils.Groups.Impl
{
    public class GameGroupUtils : IGameGroupUtils
    {
        private readonly IGroup<GameEntity> _aiGroup;
        private readonly IGroup<GameEntity> _timerGroup;
        private readonly IGroup<GameEntity> _ghostsGroup;
        private readonly IGroup<GameEntity> _obstacleGroupsGroup;
        private readonly IGroup<GameEntity> _checkpointGroup;
        private readonly IGroup<GameEntity> _collectables;
        private readonly IGroup<GameEntity> _chunks;
        private readonly IGroup<GameEntity> _characters;

        public GameGroupUtils(GameContext game)
        {
            _aiGroup = game.GetGroup(GameMatcher.AllOf(GameMatcher.Ai));
            _timerGroup = game.GetGroup(GameMatcher.AllOf(GameMatcher.Timer));
            _ghostsGroup = game.GetGroup(GameMatcher.AllOf(GameMatcher.Ghost));
            _characters = game.GetGroup(GameMatcher.AllOf(GameMatcher.Character));
            _obstacleGroupsGroup = game.GetGroup(GameMatcher.AllOf(GameMatcher.ObstacleGroup));
            _checkpointGroup = game.GetGroup(GameMatcher.AllOf(GameMatcher.Checkpoint));
            _collectables = game.GetGroup(GameMatcher.AllOf(GameMatcher.CollectableItem));
            _chunks = game.GetGroup(GameMatcher.AllOf(GameMatcher.ChunkNode));
        }

        public IDisposable GetAi(out List<GameEntity> buffer, Func<GameEntity, bool> filter = null, bool nonDestroyed = true)
        {
            // Более показать не могу
            buffer = default;
            return default;
        }

        public IDisposable GetGhosts(out List<GameEntity> buffer, Func<GameEntity, bool> filter = null)
        {
            // Более показать не могу
            buffer = default;
            return default;
        }

        public IDisposable GetCharacters(out List<GameEntity> buffer, Func<GameEntity, bool> filter = null)
        {
            // Более показать не могу
            buffer = default;
            return default;
        }

        public IDisposable GetTimers(out List<GameEntity> buffer, Func<GameEntity, bool> filter = null)
        {
            // Более показать не могу
            buffer = default;
            return default;
        }

        public IDisposable GetBerserkModeTimers(out List<GameEntity> buffer, Func<GameEntity, bool> filter = null)
            {
            // Более показать не могу
            buffer = default;
            return default;
        }

        public IDisposable GetObstacleGroups(out List<GameEntity> buffer, Func<GameEntity, bool> filter = null)
        {
            // Более показать не могу
            buffer = default;
            return default;
        }        
        
        public IDisposable GetCheckpoints(out List<GameEntity> buffer, Func<GameEntity, bool> filter = null)
        {
            // Более показать не могу
            buffer = default;
            return default;
        }
        
        public IDisposable GetCollectables(out List<GameEntity> buffer, Func<GameEntity, bool> filter = null)
            {
            // Более показать не могу
            buffer = default;
            return default;
        }

        public IDisposable GetChunks(out List<GameEntity> buffer, Func<GameEntity, bool> filter = null)
            {
            // Более показать не могу
            buffer = default;
            return default;
        }

        private IDisposable GetEntities(
            out List<GameEntity> buffer,  
            IGroup<GameEntity> group,
            Func<GameEntity, bool> baseFilter, 
            Func<GameEntity, bool> filter = null)
        {
            // Более показать не могу
            buffer = default;
            return default;
        }
    }
}