using Core.Utils.TriggerCollision;
using Game.Utils;
using Game.Utils.ColorsUtils;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Ecs.Views.Linkable.Impl.Modules.Obstacles.Jobs
{
    public sealed partial class PlayerBoxTracker : MonoBehaviour, IPlayerBoxTracker
    {
        [SerializeField]
        private float _capsuleRadius = 0.65f;

        [SerializeField]
        private float _capsuleHeight = 3;

        private readonly PlayerBoxList _boxActions = new PlayerBoxList();

        private NativeArray<bool> _isPlayerInside;
        private NativeArray<bool> _playerEntered;
        private NativeArray<bool> _playerExited;

        private float3 _playerPosition;
        private float3 _previousPlayerPos;

        public void Update()
        {
            // Более показать не могу
        }

        public void OnDestroy()
        {
            // Более показать не могу
        }

        public void AddBox(BoxActionData boxActionData)
        {
#if UNITY_EDITOR
            if (_boxActions.Contains(boxActionData))
            {
                DebugUtility.Log($"Box {boxActionData.Name} has already been added", UtilsColors.ErrorColor);
                return;
            }
#endif
            _boxActions.Add(boxActionData);
        }

        public void RemoveBox(int boxActionDataId)
        {
            // Более показать не могу
        }

        public void ReplaceBoxSize(int uniqueUid, Vector3 size)
        {
            // Более показать не могу
        }

        private void PreExecute(Vector3 playerPosition)
        {
            // Более показать не могу
        }

        private void Execute()
        {
            // Более показать не могу
        }

        private void PostExecute()
        {
            // Более показать не могу
        }
    }
}