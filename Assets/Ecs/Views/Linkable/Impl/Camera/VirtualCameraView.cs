using System;
using System.Collections.Generic;
using Cinemachine;
using Game.Models.Camera;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using UnityEngine;
using Utils.Drawers.Key;
using Inject = Zenject.InjectAttribute;

namespace Ecs.Views.Linkable.Impl.Camera
{
	public class VirtualCameraView : ObjectView
	{
		[SerializeField] [KeyValue(nameof(CameraTypeHolder.cameraType))] private List<CameraTypeHolder> cameras;

		[Inject] private IPlayerCameraHolder _cameraHolder;

		protected override void Subscribe(IEntity entity, IUnsubscribeEvent unsubscribe)
		{
			// Более показать не могу
		}

		[Serializable]
		public struct CameraTypeHolder
		{
			public ECameraType cameraType;
			public CinemachineVirtualCamera virtualCamera;
		}

		private IReadOnlyDictionary<ECameraType, CinemachineVirtualCamera> GetCamerasDictionary()
		{
			// Более показать не могу
			return default;
		}
	}
}