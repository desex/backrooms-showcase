using System.Collections.Generic;
using Core.Utils.TriggerCollision;
using Ecs.Utils;
using Ecs.Views.Linkable.Impl.Modules.Obstacles.Jobs;
using Ecs.Views.Linkable.Impl.Obstacles;
using Game.Services.Analytics;
using Game.Services.Chunks;
using Game.Utils;
using Game.Utils.Chunks;
using Generated.Commands;
using JCMG.EntitasRedux.Commands;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Chunk
{
    public partial class ChunkView : PoolObjectView
    {
        public Transform EndOnlyAnchor;

        [HideInInspector]
        public List<ChunkWallView> ChunkWalls;

        [HideInInspector]
        public List<ObstacleGroupView> Obstacles;

        [field: Required]
        [field: SerializeField]
        public Transform StartAnchor { get; private set; }

        [field: SerializeField]
        public Box EnterTrigger { get; private set; }

        [field: SerializeField]
        [field: ValidateInput("ValidateChunkLength")]
        public float ChunkLength { get; private set; }

        [field: SerializeField]
        public ObstacleSpawnPoint[] ObstacleSpawnPoints { get; private set; }

        [field: SerializeField]
        public List<ObstacleGroupView> BuiltInObstacles { get; private set; }

        [field: SerializeField]
        public Transform EndLeftAnchor { get; private set; }

        [field: SerializeField]
        public Transform EndRightAnchor { get; private set; }

        [field: Header("Collision")]
        [field: SerializeField]
        public List<Bounds> CollisionBounds { get; private set; }

        [field: SerializeField]
        public Transform[] MiddleAnchors { get; private set; }

        [field: SerializeField]
        private AObjectViewModule _drawModule;

        private IAnalyticsService _analyticsService;
        private IChunkCurvedService _chunkCurvedService;
        private ICommandBuffer _commandBuffer;
        private GameContext _game;
        private IPlayerBoxTracker _playerBoxTracker;
        private int _triggerUid;

        [Inject]
        private void Construct(
            ICommandBuffer commandBuffer,
            IAnalyticsService analyticsService,
            IChunkCurvedService chunkCurvedService,
            IPlayerBoxTracker playerBoxTracker,
            GameContext game
        )
        {
            // Более показать не могу
        }

        public override void Activate()
        {
            // Более показать не могу
        }

        public override void Deactivate()
        {
            // Более показать не могу
        }

        private void OnChunkEnterTriggerEntered(int triggerId)
        {
            // Более показать не могу
        }
    }
}