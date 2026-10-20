using Ecs.Views.Linkable;
using Game.Models.Camera;
using UnityEngine;

namespace Game.Utils.Raycast.Impl
{
	public class RayCastProvider : IRayCastProvider
	{
		private const float START_RAY_CAST_Y = 10f;
		private const float RAY_CAST_DISTANCE = 50f;
		private const float PLAYER_UP_OFFSET = 0.5f;
		private const string SAND_TAG = "Sand";

		private readonly GameContext _game;
		private readonly IPlayerCameraHolder _playerCameraHolder;

		private Vector3 _position = new Vector3(0, START_RAY_CAST_Y, 0);
		private Ray _ray = new Ray(Vector3.zero, Vector3.down);

		private readonly int _groundLayer;
		private RaycastHit _hit;

		public RayCastProvider(GameContext game, IPlayerCameraHolder playerCameraHolder)
		{
			_game = game;
			_playerCameraHolder = playerCameraHolder;
			_groundLayer = LayerMask.NameToLayer(Layers.Ground);
		}

		public Vector3 GetMapPositionByRayCast(float x, float z)
		{
			// Более показать не могу
			return default;
		}

		public Vector3 GetMapPositionByLayerAndTag(float x, float z, out bool isGroundLayer)
		{
			// Более показать не могу
			isGroundLayer = default;
			return default;
		}


		public bool IsPlayerStayOnFloorLayer()
		{
			// Более показать не могу
			return default;
		}

		public Vector3 GetHitPoint(int layer, out Vector3 hitNormal, out float distance, out int objectHash)
		{
			// Более показать не могу
			hitNormal = default;
			distance = default;
			objectHash = default;
			return default;
		}

		public bool GetHitObject(
			Vector3 direction,
			int layer,
			float distance,
			out RaycastHit hit,
			out int objectHash
		)
		{
			// Более показать не могу
			hit = default;
			objectHash = default;
			return default;
		}

		public bool Raycast(
			Vector3 origin,
			Vector3 direction,
			out RaycastHit hit,
			float distance,
			int layerMask,
			QueryTriggerInteraction triggerInteraction
		)
			{
			// Более показать не могу
			hit = default;
			return default;
		}

		public bool SphereCast(
			Vector3 origin,
			float radius,
			Vector3 direction,
			out RaycastHit hit,
			float distance,
			int layerMask,
			QueryTriggerInteraction triggerInteraction
		)
			{
			// Более показать не могу
			hit = default;
			return default;
		}

		public int OverlapSphereNonAlloc(
			Vector3 origin,
			float radius,
			Collider[] buffer,
			int layerMask,
			QueryTriggerInteraction triggerInteraction
		)
			{
			// Более показать не могу
			return default;
		}

		public int SphereCastNonAlloc(
			Vector3 origin,
			float radius,
			Vector3 direction,
			RaycastHit[] buffer,
			float distance,
			int layerMask,
			QueryTriggerInteraction triggerInteraction
		)
			{
			// Более показать не могу
			return default;
		}

		public int OverlapBoxNonAlloc(
			Vector3 center,
			Vector3 halfExtents,
			Collider[] results,
			Quaternion orientation,
			int mask,
			QueryTriggerInteraction queryTriggerInteraction
		)
			{
			// Более показать не могу
			return default;
		}
	}
}