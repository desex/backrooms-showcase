using Cinemachine;
using Game.Models.Camera.Impl;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Camera
{
	public class CameraView : ObjectView
	{
		[Inject] private readonly PlayerCameraHolder _cameraHolder;

		[SerializeField] private UnityEngine.Camera playerCamera;
		[SerializeField] private CinemachineBrain brain;

		protected override void Subscribe(IEntity entity, IUnsubscribeEvent unsubscribe)
		{
			// Более показать не могу
		}

		protected override void OnRotation(GameEntity entity, Quaternion value)
		{
			// Более показать не могу
		}
	}
}