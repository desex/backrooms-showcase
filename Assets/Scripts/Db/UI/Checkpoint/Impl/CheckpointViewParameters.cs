using Sirenix.OdinInspector;
using UnityEngine;

namespace Db.UI.Checkpoint.Impl
{
    [CreateAssetMenu(menuName = "Settings/UI/" + nameof(CheckpointViewParameters), fileName = nameof(CheckpointViewParameters))]
    public class CheckpointViewParameters : ScriptableObject, ICheckpointViewParameters
    {
        [field: SerializeField] public CheckpointUiAnimationVo AnimationVo { get; private set; }
        
        [field: Title("Texts")]
        [field: SerializeField] public string Text { get; private set; }
    }
}