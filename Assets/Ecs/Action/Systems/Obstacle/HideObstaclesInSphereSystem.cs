using System.Threading;
using Db.Player;
using Ecs.Action.Commands.Obstacles;
using Ecs.Utils;
using Ecs.Utils.Groups;
using Ecs.Views.Linkable.Impl.Chunk;
using Ecs.Views.Linkable.Impl.Modules.Revive;
using Game.Services.Chunks;
using Game.Services.Obstacles;
using Game.Utils;
using Game.Utils.Overlaps;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;
using Unity.Collections;

namespace Ecs.Action.Systems.Obstacle
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 667, nameof(EFeatures.Common))]
    public class HideObstaclesInSphereSystem : ForEachCommandUpdateSystem<HideObstaclesInSphereCommand>
    {
        private readonly IOverlapService _overlapService;
        private readonly IGameGroupUtils _gameGroupUtils;
        private readonly IObstacleService _obstacleService;        
        private readonly IPlayerDeathParametersBase _playerDeathParametersBase;
        private readonly ISequentialObstacleService _sequentialObstacleService;

        public HideObstaclesInSphereSystem(
            IObstacleService obstacleService,
            IGameGroupUtils gameGroupUtils,
            IOverlapService overlapService,
            ICommandBuffer commandBuffer,
            ISequentialObstacleService sequentialObstacleService
        ) : base(commandBuffer)
        {
            _obstacleService = obstacleService;
            _gameGroupUtils = gameGroupUtils;
            _overlapService = overlapService;
            _sequentialObstacleService = sequentialObstacleService;
        }

        protected override void Execute(ref HideObstaclesInSphereCommand command)
        {
            using var _ = _overlapService.OverlapSphere<ReviveObjectHiderModule>(
                command.Position, command.Radius, LayerMasks.GameObjectHider, out var overlapSphereList);
            
            foreach (var hider in overlapSphereList)
            {   
                hider.Entity.IsHided = true;
                if (hider.HasWalls)
                {
                    hider.DisableObjects();
                }
                else
                {
                    if (hider.Entity.HasSequencePrefabId)
                        _sequentialObstacleService.DespawnObstacle(hider.ObstacleGroupView, CancellationToken.None);
                    else
                        _obstacleService.DespawnObstacle(hider.ObstacleGroupView, CancellationToken.None);

                    ClearLinks(hider);
                }
            }
        }

        private void ClearLinks(ReviveObjectHiderModule hiderModule)
        {
            using var _ = _gameGroupUtils.GetChunks(out var chunks);

            foreach (var chunk in chunks)
            {
                var view = (ChunkView)chunk.Link.View;

                for (var i = view.Obstacles.Count - 1; i >= 0; i--)
                {
                    if (view.Obstacles[i] == hiderModule.ObstacleGroupView)
                        view.Obstacles.RemoveAtSwapBack(i);
                }
            }
        }
    }
}