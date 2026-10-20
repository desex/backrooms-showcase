using Core.Utils.TriggerCollision;
using Ecs.Views.Linkable.Impl.Modules.Obstacles.Jobs;
using Game.Utils;
using Game.Utils.Movement;
using Generated.Commands;
using JCMG.EntitasRedux.Commands;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Movement
{
    public partial class WallRunTriggerView : MonoBehaviour
    {
        [SerializeField]
        private bool _isEnabled;

        [ShowIf("_isEnabled")]
        [SerializeField]
        private ETiltType _tiltType;

        [ShowIf("_isEnabled")]
        [SerializeField]
        private Transform _endPoint;

        [SerializeField] private Box _trigger;

        
        private ICommandBuffer _commandBuffer;
        private GameContext _game;
        private IPlayerBoxTracker _playerBoxTracker;

        private int _triggerUid;

        [Inject]
        private void Construct(
            ICommandBuffer commandBuffer,
            IPlayerBoxTracker playerBoxTracker,
            GameContext game
        )
        {
            _commandBuffer = commandBuffer;
            _playerBoxTracker = playerBoxTracker;
            _game = game;
        }

        private void OnEnable()
        {
            var thisTransform = transform;

            _triggerUid = TriggerUidGenerator.Next();

            var turnBoxActionData = new BoxActionData
            {
                Box = _trigger,
                Name = $"{name} - {nameof(WallRunTriggerView)}",
                GlobalPosition = thisTransform.TransformPoint(_trigger.Center),
                GlobalRotation = thisTransform.rotation * Quaternion.Euler(_trigger.Rotation),
                Uid = _triggerUid,
                EnterAction = SetReadyWallRun,
                ExitAction = UnsetReadyWallRun,
            };

            _playerBoxTracker.AddBox(turnBoxActionData);
        }
        
        public void SetTiltType(ETiltType tiltType)
        {
            _tiltType = tiltType;
        }

        private void SetReadyWallRun()
        {
            if (!_isEnabled)
                return;

            var delta = (_endPoint.position - _game.PlayerEntity.Position.Value).NoY();
            var distance = Mathf.Sqrt(delta.x * delta.x + delta.z * delta.z);
            var characterUid = _game.PlayerEntity.Uid.Value;
            _commandBuffer.SetReadyWallRun(characterUid, true, distance);

            _game.PlayerEntity.ReplaceCharacterTouchWall(_tiltType);
        }

        private void UnsetReadyWallRun()
        {
            if (_game.PlayerEntity.HasCharacterTouchWall)
                _game.PlayerEntity.RemoveCharacterTouchWall();
        }

        private void OnDisable()
        {
            _playerBoxTracker.RemoveBox(_triggerUid);
        }
    }
}