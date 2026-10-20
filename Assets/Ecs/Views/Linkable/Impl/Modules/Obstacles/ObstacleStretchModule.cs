using System;
using System.Collections.Generic;
using Ecs.Utils;
using Ecs.Utils.Groups;
using Game.Utils;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using R3;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.Obstacles
{
    public class ObstacleStretchModule : AObjectViewModule
    {
        private enum EStretchMode
        {
            Repeat = 0,
            Scale = 1,
        }
        
        private const float MAX_RAYCAST_DISTANCE = 30;
        private const float TARGET_LENGTH_THRESHOLD = 0.1f;

        private readonly Vector3 _pooledRepeatBlocksPosition = new Vector3(9999f, 9999f, 9999f);
        private readonly RaycastHit[] _raycastHit = new RaycastHit[1];
        private readonly List<Transform> _usedRepeatBlocks = new();

        [Header("Stretching")]
        [SerializeField] private BoxCollider[] _collidersToUpdate;
        
        [Header("Border Parts")]
        [SerializeField] private Transform _leftEndPart;
        [SerializeField] private Transform _rightEndPart;

        [Header("Stretching")] 
        [SerializeField] private EStretchMode _stretchMode;

        [ShowIf(nameof(IsRepeatModeSelected))]
        [SerializeField] private Transform _blockToRepeat;
        
        [ShowIf(nameof(IsRepeatModeSelected))]
        [SerializeField] private List<Transform> _availableRepeatBlocks;
        
        [ShowIf(nameof(IsRepeatModeSelected))]
        [InfoBox("Container must be a child of ObstacleView object and have zero position!", InfoMessageType.Warning)]
        [SerializeField] 
        private Transform _blocksToRepeatContainer;

        [ShowIf(nameof(IsRepeatModeSelected))]
        [SerializeField] private float _blockToRepeatLength;

        [ShowIf(nameof(IsRepeatModeSelected))]
        [InfoBox("Optional")]
        [SerializeField] private MeshRandomColorSetter _meshRandomColorSetter;
        
        [ShowIf(nameof(IsScaleModeSelected))]
        [SerializeField] private Transform _blockToScale;

        [ShowIf(nameof(IsScaleModeSelected))]
        [SerializeField] private float _blockToScaleLength;
        
        private IGameGroupUtils _gameGroupUtils;

        private Vector3 _leftOriginalLocalPosition, _rightOriginalLocalPosition;
        private IDisposable _disposeUpdateObstacleSize;
        private IUnsubscribeEvent _unsubscribe;
        private GameEntity _self;

        [Inject]
        private void Construct(IGameGroupUtils gameGroupUtils)
        {
            // Более показать не могу
        }

        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
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

        private bool IsRepeatModeSelected() {
            // Более показать не могу
            return default;
        }
        private bool IsScaleModeSelected() {
            // Более показать не могу
            return default;
        }

        private void Awake()
        {
            // Более показать не могу
        }

        private bool TryUpdateObstacleSize()
        {
            // Более показать не могу
            return default;
        }

        private void StretchCollidersAndBoxes(float targetLength)
        {
            // Более показать не могу
        }

        private (Vector3 position, Vector3 normal) FindEndPoint(Vector3 startPoint, Vector3 direction)
        {
            // Более показать не могу
            return default;
        }

        private void ApplyNeedLenghtByScaling(float targetLenght)
        {
            // Более показать не могу
        }

        private void ApplyNeedLenghtByRepeating(float targetLenght)
        {
            // Более показать не могу
        }

        private Transform GetFreeRepeatBlock()
        {
            // Более показать не могу
            return default;
        }

        private void ResetRepeatBlock(Transform repeatBlock)
        {
            // Более показать не могу
        } 
    }
}