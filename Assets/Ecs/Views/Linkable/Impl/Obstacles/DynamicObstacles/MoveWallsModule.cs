using System;
using Db.SoundFxBase;
using DG.Tweening;
using Ecs.Utils;
using FMODUnity;
using Game.Services.Sound;
using Game.Utils;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using R3;
using R3.Triggers;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Obstacles.DynamicObstacles
{
    public class MoveWallsModule : AObjectViewModule
    {
        [SerializeField] private Collider _startMoveCollider;
        
        [Header("End points")]
        [SerializeField] private Transform _leftWallEndPoint;
        [SerializeField] private Transform _rightWallEndPoint;

        [Header("Walls")]
        [SerializeField] private Transform _leftWall;
        [SerializeField] private Transform _rightWall;

        [Header("Animation")]
        [SerializeField] private float _moveDuration = 1f;
        [SerializeField] private Ease _moveEase = Ease.Linear;

        private Sequence _sequence;
        private Vector3? _leftWallOriginLocalPosition;
        private Vector3? _rightWallOriginLocalPosition;
        private StudioEventEmitter _moveSound;
        private ISoundService _soundService;

        [Inject]
        public void Construct(ISoundService soundService)
        {
            // Более показать не могу
        }

        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }

        public override void Deactivate()
        {
            // Более показать не могу
        }

        private void OnStartMoveTriggerEnter(Collider other)
        {
            // Более показать не могу
        }

        private Tween CreateMoveWallTween(Transform wallTransform, Transform rawDestinationPoint, Vector3 targetDirection)
        {
            // Более показать не могу
            return default;
        }

        private void ResetWallsPositions()
        {
            // Более показать не могу
        }
        
        private void SetUpEmitter(StudioEventEmitter emitter)
        {
            // Более показать не могу
        }
    }
}