using System.Collections.Generic;
using Cinemachine;
using Game.Utils.Camera;
using UnityEngine;

namespace Game.Models.Camera
{
	public interface IPlayerCameraHolder : ICameraHolder
	{
		Transform AnchorsContainer { get; }
		void SetPriorityCamera(ECameraType cameraType);
		void Init(IReadOnlyDictionary<ECameraType, CinemachineVirtualCamera> cameras);
		void Update();
		void SetCameraAnchor(CameraAnchor cameraAnchor);
		void SetCameraAnchorsContainer(Transform anchorsContainer);
		CinemachineVirtualCamera GetCameraByType(ECameraType cameraType);
		void SwitchToMainMenuCamera();
		void SwitchToGamePlayCamera();
		float GetCamerasFov();
		void SetAllCamerasFov(float fov);
		void SetLookAtTargetInCamera(ECameraType cameraType, Transform lookAtTransform);
	}
}