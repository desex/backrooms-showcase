using UnityEngine;

namespace Game.Utils
{
	public class LayerMasks
	{
		private static readonly Mask PlayerTriggerMask = new Mask(Layers.PlayerTriger);
		private static readonly Mask GameObjectHiderMask = new Mask(Layers.GameObjectsHider);
		private static readonly Mask BreakBoxMask = new Mask(Layers.BreakBox);
		private static readonly Mask CharacterMask = new Mask(Layers.PlayerTriger, Layers.GhostTrigger);
		private static readonly Mask GhostMask = new Mask(Layers.GhostTrigger);
		private static readonly Mask InteractableMask = new Mask(Layers.Interactable);
		private static readonly Mask GroundMask = new Mask(Layers.Ground);
		private static readonly Mask FloorMask = new Mask(Layers.Floor);
		private static readonly Mask InteractableAndAiMask = new Mask(Layers.Interactable, Layers.Ai);
        private static readonly Mask Grounded = new Mask(Layers.Ground, Layers.Floor, Layers.Chunk);

		private static readonly Mask GhostsNonWalkableMask = new Mask(Layers.Ground,
		                                                              Layers.Floor,
		                                                              Layers.Chunk,
		                                                              Layers.Obstacle,
		                                                              Layers.ObstacleGroup,
		                                                              Layers.GhostsPathBlock,
		                                                              Layers.Default
		);
		private static readonly Mask ChunkWallMask = new Mask(Layers.Default, Layers.Floor, Layers.Chunk);
		private static readonly Mask ObstacleMask = new Mask(Layers.Obstacle);
		private static readonly Mask ObstacleGroupMask = new Mask(Layers.ObstacleGroup);
		private static readonly Mask LampMask = new Mask(Layers.Lamp);
		private static readonly Mask ChunkMask = new Mask(Layers.Chunk, Layers.Obstacle);
		private static readonly Mask CollectablesMask = new Mask(Layers.Collectables);

		private static readonly Mask AttackableMask =
			new Mask(Layers.PlayerTriger, Layers.Default, Layers.Ground, Layers.Floor, Layers.Interactable);

		private static readonly Mask AllMask = new Mask(Layers.Default, Layers.PlayerTriger, Layers.Interactable,
			Layers.Ground, Layers.Floor, Layers.Ai);

		private static readonly Mask AllWithoutPlayerMask = new Mask(Layers.Default, Layers.Interactable,
			Layers.Ground, Layers.Floor, Layers.Ai);

		public static int Chunk => ChunkMask.Value;
		public static int GameObjectHider => GameObjectHiderMask.Value;
		public static int Lamp => LampMask.Value;
		public static int PlayerTrigger => PlayerTriggerMask.Value;
		public static int BreakBox => BreakBoxMask.Value;
		public static int Character => CharacterMask.Value;
		public static int Ghost => GhostMask.Value;
		public static int Interactable => InteractableMask.Value;
        public static int GroundedMask => Grounded.Value;
		public static int Ground => GroundMask.Value;
		public static int Floor => FloorMask.Value;
		public static int InteractableAndAi => InteractableAndAiMask.Value;
		public static int All => AllMask.Value;
		public static int AllWithoutPlayer => AllWithoutPlayerMask.Value;
		public static int Attackable => AttackableMask.Value;
		public static int ChunkWall => ChunkWallMask.Value; // TODO: rename
		public static int Obstacle => ObstacleMask.Value;
		public static int ObstacleGroup => ObstacleGroupMask.Value;
		public static int Collectables => CollectablesMask.Value;
		public static int GhostsNonWalkable => GhostsNonWalkableMask.Value;

		public static int HitTargets
		{
			get
			{
				var ai = 1 << Layers.AiLayer;
				var player = 1 << Layers.PlayerTriggerLayer;
				var ignoreRaycast = 1 << Layers.IgnoreRaycastLayer;
				return ~(ai | player | ignoreRaycast);
			}
		}

		private class Mask
		{
			private readonly string[] _layerNames;

			private int? _value;

			public Mask(params string[] layerNames)
			{
				_layerNames = layerNames;
			}

			public int Value
			{
				get
				{
					if (!_value.HasValue)
						_value = LayerMask.GetMask(_layerNames);
					return _value.Value;
				}
			}
		}
	}
}