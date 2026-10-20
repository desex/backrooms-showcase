using Game.Models.Camera;
using UnityEngine;

namespace Game.Services.Camera
{
    public interface ICameraService
    {
        ECameraType ActiveCameraType { get; }
        void SetActiveCamera(ECameraType cameraType);
        void PlayDamageAnimation(Vector3 directionToDamageSource);
        void SetLookAtTargetInCamera(ECameraType cameraType, Transform lookAtTransform);
        void RotateCameraUp();
        void RotateCameraToDefault();
    }
}