using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Db.Obstacles.DestructibleObstacles.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(DestructibleObstaclesParameters), fileName = nameof(DestructibleObstaclesParameters))]
    public class DestructibleObstaclesParameters : ScriptableObject, IDestructibleObstaclesParameters
    {
        private const string PLASTIC_GROUP = "Plastic";
        
        [field: BoxGroup (PLASTIC_GROUP, true, true)]
        [field: Tooltip("The speed of player after passing plastic in percents relatively to speed before passing")]
        [field: SerializeField]
        public float TargetAcceleration { get; private set; }

        [field: BoxGroup (PLASTIC_GROUP)]
        [field: Tooltip("The acceleration duration")]
        [field: Range(0f,2f)]
        [field: SerializeField]
        public float AccelerationDuration { get; private set; }

        [field: BoxGroup (PLASTIC_GROUP, true, true)]
        [field: Tooltip("The ease of acceleration")]
        [field: SerializeField]
        public Ease AccelerationEase { get; private set; }
    }
}