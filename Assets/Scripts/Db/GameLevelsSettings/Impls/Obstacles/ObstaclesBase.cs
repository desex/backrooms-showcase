using System.Collections.Generic;
using Db.GenerationSettings;
using Ecs.Views.Linkable.Impl.Obstacles;
using Game.Utils.Obstacles;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Db.GameLevelsSettings.Impls.Obstacles
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(ObstaclesBase), fileName = nameof(ObstaclesBase))]
    public sealed class ObstaclesBase : SerializedScriptableObject, IObstaclesBase
    {
#if UNITY_EDITOR
        [Title("Wall parts")]
        [LabelText("Wall parts dictionary")]
        [OdinSerialize]
        [HideReferenceObjectPicker]
        [Searchable]
        private Dictionary<EObstacleType, SerializedObstacleTypeInfo> _obstaclesMatrices;
#endif
        
        [OdinSerialize]
        [HideInInspector]
        private Dictionary<EObstacleType, IReadOnlyList<ObstacleGroupView>> _obstacles;
        
        [OdinSerialize]
        [HideInInspector]
        private Dictionary<EObstacleType, ObstacleTypeInfo> _obstacleInfos;
        
        public IReadOnlyDictionary<EObstacleType, IReadOnlyList<ObstacleGroupView>> Obstacles => _obstacles;
        public IReadOnlyDictionary<EObstacleType, ObstacleTypeInfo> ObstacleInfos => _obstacleInfos;
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            _obstacleInfos = new Dictionary<EObstacleType, ObstacleTypeInfo>();
            
            foreach (var pair in _obstaclesMatrices)
            {
                _obstacleInfos[pair.Key] = pair.Value;
            }
            
            _obstacles = new Dictionary<EObstacleType, IReadOnlyList<ObstacleGroupView>>();

            foreach (var pair in _obstacleInfos)
            {
                _obstacles[pair.Key] = pair.Value;
            }
        }
#endif
    }
}