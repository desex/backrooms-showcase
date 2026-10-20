using UnityEngine;

namespace Db.Player.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(PlayerSpeedProgressionParameters), fileName = nameof(PlayerSpeedProgressionParameters))]
    public class PlayerSpeedProgressionParameters : ScriptableObject, IPlayerSpeedProgressionParameters
    {
        [field: SerializeField] public float InitialForwardSpeedMultiplier { get; private set; } = 1f;
        
        [field: Header("Progression in percent")]
        [field: Range(0f,1f)] 
        [field : SerializeField] 
        public float ProgressionMultiply { get; private set; }
        
        [field: Range(1f,2f)] 
        [field : SerializeField] 
        public float MaxProgressionMultiply { get; private set; }
        
        [field: Range(0f,1.5f)] 
        [field : SerializeField] 
        public float InitialRunAnimationSpeed { get; private set; }

        [field: Range(1f,2f)] 
        [field : SerializeField] 
        public float MaxRunAnimationMultiply { get; private set; }
        
        [field: Header("Progression in meters")]
        [field: SerializeField] public float MetersProgression { get; private set; }
    }
}