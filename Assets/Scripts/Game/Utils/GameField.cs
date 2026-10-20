using System.Collections.Generic;
using AmazingAssets.CurvedWorld;
using BehaviorDesigner.Runtime;
using Ecs.Views.Linkable.Impl.Chunk;
using Ecs.Views.Linkable.Impl.Obstacles;
using Ecs.Views.Linkable.Impl.Player;
using Sirenix.OdinInspector;
using StylizedWater2.UnderwaterRendering;
using UnityEngine;

namespace Game.Utils
{
    public class GameField : SerializedMonoBehaviour
    {
        private const string GENERAL_GROUP = "General";

        [field: TabGroup(GENERAL_GROUP)]
        [field: SerializeField]
        public ObstacleGroupView[] ObstacleGroupViews { get; private set; }

        [field: TabGroup(GENERAL_GROUP)]
        [field: SerializeField]
        public ChunkView[] ChunkViews { get; private set; }

        [field: TabGroup(GENERAL_GROUP)]
        [field: SerializeField]
        public PlayerView PlayerView { get; private set; }

        [field: TabGroup(GENERAL_GROUP)]
        [field: SerializeField]
        public Transform ChunksRoot { get; private set; }

        [field: TabGroup(GENERAL_GROUP)]
        [field: SerializeField]
        public Transform ObstaclesRoot { get; private set; }

        [field: TabGroup(GENERAL_GROUP)]
        [field: SerializeField]
        public Transform LampsRoot { get; private set; }

        [field: TabGroup(GENERAL_GROUP)]
        [field: SerializeField]
        public Transform CollectablesRoot { get; private set; }  
        
        [field: TabGroup(GENERAL_GROUP)]
        [field: SerializeField]
        public Transform CheckpointsRoot { get; private set; }        
        
        [field: TabGroup(GENERAL_GROUP)]
        [field: SerializeField]
        public Transform GhostsRoot { get; private set; }

        //TODO: refactor this
        [field: TabGroup(GENERAL_GROUP)]
        [field: SerializeField]
        public BehaviorManager BehaviorManager { get; private set; }
        
        [field: TabGroup(GENERAL_GROUP)]
        [field: SerializeField]
        public Dictionary<EGameLevelType, UnderwaterRenderer> UnderwaterRenderersByLevel { get; private set; }
        
        [field: TabGroup(GENERAL_GROUP)]
        [field: SerializeField]
        public CurvedWorldController CurvedWorldController { get; private set; }    

        [Button]
        private void AutoFill()
        {
            // Более показать не могу
        }

        [Button]
        private void ClearFields()
        {
            // Более показать не могу
        }
    }
}