using Ecs.Views.Linkable;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Db.Obstacles.Impls
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(ObstacleParametersBase), fileName = nameof(ObstacleParametersBase))]
    public class ObstacleParametersBase : ScriptableObject, IObstacleParametersBase
    {
        private const string SEARCH_FOLDER = "Assets/";
        
        [field: SerializeField] public float ReactivationDelay { get; private set; }
        
        [field: SerializeField] public float ReadyTriggerOffset { get; private set; }
        [field: SerializeField] public float ReadyTriggerLength { get; private set; }
        
        [field: SerializeField] public float FastMoveTriggerOffset { get; private set; }
        [field: SerializeField] public float FastMoveTriggerLength { get; private set; }

        #if UNITY_EDITOR
        [Button("Update Values In Prefabs")]
        private void UpdateValuesInPrefabs()
        {
            // Более показать не могу
        }

        private void UpdateValuesInTrigger(IFastActionTrigger slowActionTrigger)
        {
            // Более показать не могу
        }
        #endif
    }
}