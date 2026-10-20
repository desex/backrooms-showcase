using UnityEngine;

namespace Game.Utils
{
	public class Layers
	{
		public const string Default = "Default";
		public const string PlayerTriger = "PlayerTrigger";
		public const string Player = "Player";
		public const string GhostTrigger = "GhostTrigger";
		public const string Ground = "Ground";
		public const string Floor = "Floor";
		public const string Ai = "Ai";
		public const string Interactable = "Interactable";
		public const string IgnoreRaycast = "Ignore Raycast";
		public const string Obstacle = "Obstacle";
		public const string ObstacleGroup = "ObstacleGroup";
		public const string Lamp = "Lamp";
		public const string Chunk = "Chunk";
		public const string Collectables = "Collectables";
		public const string GhostsPathBlock = "GhostsPathBlock";
		public const string BreakBox = "BreakBox";
		public const string GameObjectsHider = "GameObjectHider";

		private static readonly Layer _playerTriggerLayer = new Layer(PlayerTriger);
		private static readonly Layer _playerLayer = new Layer(Player);
		private static readonly Layer _interactableLayer = new Layer(Interactable);
		private static readonly Layer _defaultLayer = new Layer(Default);
		private static readonly Layer _floorLayer = new Layer(Floor);
		private static readonly Layer _aiLayer = new Layer(Ai);
		private static readonly Layer _ignoreRaycast = new Layer(IgnoreRaycast);
		private static readonly Layer _obstacleLayer = new Layer(Obstacle);
		private static readonly Layer _obstacleGroupLayer = new Layer(ObstacleGroup);
		private static readonly Layer _lampLayer = new Layer(Lamp);
		private static readonly Layer _chunkLayer = new Layer(Chunk); 
		private static readonly Layer _collectables = new Layer(Collectables);
		private static readonly Layer _ghostsPathBlockLayer = new Layer(GhostsPathBlock);
		private static readonly Layer _breakBoxLayer = new Layer(BreakBox);
		private static readonly Layer _gameObjectsHider = new Layer(GameObjectsHider);

		public static int PlayerTriggerLayer => _playerTriggerLayer.Id;
		public static int PlayerLayer => _playerLayer.Id;
		public static int InteractableLayer => _interactableLayer.Id;
		public static int DefaultLayer => _defaultLayer.Id;
		public static int FloorLayer => _floorLayer.Id;
		public static int AiLayer => _aiLayer.Id;
		public static int IgnoreRaycastLayer => _ignoreRaycast.Id;
		public static int ObstacleLayer => _obstacleLayer.Id;
		public static int ObstacleGroupLayer => _obstacleGroupLayer.Id;
		public static int LampLayer => _lampLayer.Id;
		public static int ChunkLayer => _chunkLayer.Id;
		public static int GhostsPathBlockLayer => _ghostsPathBlockLayer.Id;
		public static int BreakBoxLayer => _breakBoxLayer.Id;
		public static int GameObjectsHiderLayer => _gameObjectsHider.Id;

		private class Layer
		{
			private readonly string _name;

			private int? _id;

			public int Id
			{
				get
				{
					if (!_id.HasValue)
						_id = LayerMask.NameToLayer(_name);
					return _id.Value;
				}
			}

			public Layer(string name)
			{
				_name = name;
			}
		}
	}
}