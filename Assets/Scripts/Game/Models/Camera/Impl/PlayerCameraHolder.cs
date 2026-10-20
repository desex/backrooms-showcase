using System.Collections.Generic;
using Cinemachine;
using Game.Utils.Camera;
using UnityEngine;

namespace Game.Models.Camera.Impl
{
    public class PlayerCameraHolder : IPlayerCameraHolder
    {
        private enum ECurrentCamera
        {
            Gameplay = 0,
            MainMenu = 1,
        }

        private const int ACTIVE_PRIORITY = 10;
        private const int INACTIVE_PRIORITY = 0;

        private UnityEngine.Camera _camera;
        private UnityEngine.Camera _mainMenuCamera;
        private CinemachineBrain _brain;
        private IReadOnlyDictionary<ECameraType, CinemachineVirtualCamera> _cameras;
        private ECurrentCamera _currentCamera;

        public Transform AnchorsContainer { get; private set; }

        public UnityEngine.Camera GetCamera() {
            // Более показать не могу
            return default;
        }

        public void SetCamera(UnityEngine.Camera camera)
        {
            // Более показать не могу
        }

        public void SetLookAtTargetInCamera(ECameraType cameraType, Transform lookAtTransform)
        {
            // Более показать не могу
        } 

        public void SetMainMenuCamera(UnityEngine.Camera camera)
        {
            // Более показать не могу
        }

        public void SetBrain(CinemachineBrain brain)
        {
            // Более показать не могу
        }

        public void SetPriorityCamera(ECameraType cameraType)
        {
            // Более показать не могу
        }

        public void Init(IReadOnlyDictionary<ECameraType, CinemachineVirtualCamera> cameras)
        {
            // Более показать не могу
        }

        public void Update()
        {
            // Более показать не могу
        }

        public void SetCameraAnchor(CameraAnchor cameraAnchor)
        {
            // Более показать не могу
        }

        public void SetCameraAnchorsContainer(Transform anchorsContainer)
        {
            // Более показать не могу
        }

        public CinemachineVirtualCamera GetCameraByType(ECameraType cameraType) {
            // Более показать не могу
            return default;
        }

        public void SwitchToMainMenuCamera()
        {
            // Более показать не могу
        }

        public void SwitchToGamePlayCamera()
        {
            // Более показать не могу
        }

        private void SetAllCameraAnchors(CameraAnchor cameraAnchor)
        {
            // Более показать не могу
        }

        private void SetMainMenuCameraPositionAndRotation(CameraAnchor cameraAnchor)
        {
            // Более показать не могу
        }

        public void TurnOffGameplayCamera()
        {
            // Более показать не могу
        }

        public void SetAllCamerasFov(float fov)
        {
            // Более показать не могу
        }

        public float GetCamerasFov()
        {
            // Более показать не могу
            return default;
        }
    }
}