#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using Db.GameLevelsSettings.Impls.Obstacles;
using Ecs.Views.Linkable.Impl.Obstacles;
using Game.Utils.Obstacles;
using Game.Utils.PrefabMatrix;
using UnityEditor;
using UnityEngine;

namespace Tools
{
    public class ObstaclesBaseMerger : EditorWindow
    {
        private ObstaclesBase _baseA;
        private ObstaclesBase _baseB;
        private ObstaclesBase _result;
        
        private string _errorMessage;

        [MenuItem("Tools/Merge ObstaclesBase")]
        public static void ShowWindow()
        {
            // Более показать не могу
        }

        private void OnGUI()
        {
            // Более показать не могу
        }

        private static void MergeObstaclesBases(ObstaclesBase baseA, ObstaclesBase baseB, ObstaclesBase result)
        {
            // Более показать не могу
        }
        
        private static void MergeFrom(ObstaclesBase source, Dictionary<EObstacleType, SerializedObstacleTypeInfo> targetDict)
        {
            // Более показать не могу
        }

        private static void ApplyMergedResultToTarget(
            ObstaclesBase target,
            Dictionary<EObstacleType, SerializedObstacleTypeInfo> resultDict
        )
        {
            // Более показать не могу
        }

        private static Dictionary<EObstacleType, SerializedObstacleTypeInfo> GetSerializedMatrix(ObstaclesBase so)
        {
            // Более показать не могу
            return default;
        }

        private static SerializedObstacleTypeInfo Clone(SerializedObstacleTypeInfo original)
        {
            // Более показать не могу
            return default;
        }
    }
}
#endif