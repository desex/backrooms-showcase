using System;
using System.Collections.Generic;
using Ecs.Managers;
using Ecs.Utils;
using Game.Utils;
using Game.Utils.Characters;
using Game.Utils.Ghost;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using R3;
using R3.Triggers;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.Obstacles
{
    public class GhostsSafeRouteModule : AObjectViewModule
    {
        private const float MAX_RAYCAST_DISTANCE = 20;
        
        private readonly List<Uid> _unhandledGhosts = new();

        [InfoBox("Optional")]
        [SerializeField] 
        private Transform _leftBorderSafePoint;
        
        [InfoBox("Optional")]
        [SerializeField] 
        private Transform _rightBorderSafePoint;
        
        [SerializeField] private Transform _obstacleNonGroundedCenterPoint;
        [SerializeField] private List<GhostSafeZonePointsData> _safeZones;
        [SerializeField] private Collider[] _avoidanceTriggers;
        
        
        private ILinkedEntityRepository _linkedEntityRepository;
        private GameContext _game;
        private IDisposable _disposeUpdateSafeZones;
        private GhostAvoidanceZonesData _avoidanceZonesData;

        [Inject]
        private void Construct(
            ILinkedEntityRepository linkedEntityRepository,
            GameContext game
        )
        {
            // Более показать не могу
        }

        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }

        private bool TryUpdateSafeZones()
        {
            // Более показать не могу
            return default;
        }

        private void OnAvoidanceTriggerEnter(Collider other)
        {
            // Более показать не могу
        }

        private void OnAvoidanceTriggerExit(Collider other)
        {
            // Более показать не могу
        }

        private bool TryGetGhostEntity(Collider other, out GameEntity entity)
        {
            // Более показать не могу
            entity = default;
            return default;
        }

        private bool TryCreateBorderSafeZone(Vector3 borderPoint, out GhostSafeZonePositionsData foundEndPoint)
        {
            // Более показать не могу
            foundEndPoint = default;
            return default;
        }
    }
}