using UnityEngine;

namespace Db.Transitions.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(TransitionSettingsBase), fileName = nameof(TransitionSettingsBase))]
    public class TransitionSettingsBase : ScriptableObject, ITransitionSettingsBase
    {
        [field: SerializeField] 
        [field: Tooltip("Time to transit to another level after pipe trigger entered")]
        public float MaxPipeTransitionTime { get; private set; } = 4.2f;
        
        [field: SerializeField] 
        [field: Tooltip("Sliding time after player instantiated in pipe after transition")]
        public float MaxSlidingInPipeDuration { get; private set; } = 3.56f;
        
        [field: SerializeField] public int FramesWithoutMeshes { get; private set; } = 11;
        
        [field: SerializeField] public int FramesBeforeSlide { get; private set; } = 2;

        [field: SerializeField] 
        [field: Tooltip("Player start position in pipe after transition")]
        public Vector3 PlayerAfterTransitionStartPosition { get; private set; } = new(-1.5f, 20.5f, -22.2f);
        
        [field: SerializeField] 
        [field: Tooltip("Player start sliding position in pipe after transition")]
        public Vector3 PlayerSlidingPosition { get; private set; } = new(0, 68f, -74f);
    }
}