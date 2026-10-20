using System;
using Db.Camera;
using Db.Player;
using DG.Tweening;
using Game.Models.Camera;
using UnityEngine;

namespace Game.Services.Camera.Impl
{
    public class CameraService : ICameraService, IDisposable
    {
        private readonly ICameraParameters _cameraParameters;
        private readonly GameContext _game;
        private readonly IPlayerCameraHolder _playerCameraHolder;
        private readonly IReboundParameters _reboundParameters;
        private Tween _currentCameraFOVTween;

        private Tween _currentCameraMovementTween;
        private float _defaultAnchorsContainerRotationX;

        public CameraService(
            IPlayerCameraHolder playerCameraHolder,
            IReboundParameters reboundParameters,
            ICameraParameters cameraParameters,
            GameContext game
        )
        {
            _playerCameraHolder = playerCameraHolder;
            _reboundParameters = reboundParameters;
            _cameraParameters = cameraParameters;
            _game = game;
        }

        public ECameraType ActiveCameraType { get; private set; }

        public void SetActiveCamera(ECameraType cameraType)
        {
            // Более показать не могу
        }

        public void SetLookAtTargetInCamera(ECameraType cameraType, Transform lookAtTransform)
        {
            // Более показать не могу
        }

        public void PlayDamageAnimation(Vector3 directionToDamageSource)
        {
            // Более показать не могу
        }

        public void RotateCameraUp()
        {
            // Более показать не могу
        }

        public void RotateCameraToDefault()
        {
            // Более показать не могу
        }

        public void Dispose()
        {
            // Более показать не могу
        }

        private void SmoothlyRotateCameraByY(float angleX)
        {
            // Более показать не могу
        }
    }
}