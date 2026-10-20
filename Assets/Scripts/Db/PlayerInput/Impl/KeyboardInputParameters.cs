#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
using Sirenix.OdinInspector;
using UnityEngine;

namespace Db.PlayerInput.Impl
{
    [CreateAssetMenu(menuName = "Settings/Input/" + nameof(KeyboardInputParameters), fileName = nameof(KeyboardInputParameters))]
    public class KeyboardInputParameters : ScriptableObject, IKeyboardInputParameters
    {
        [field: Title("Basic parameters")]
        [field: SerializeField] public bool IsEnabled { get; private set; } = true;

        [field: Title("Special moves parameters")]
        [field: SerializeField] 
        public KeyCode DashActionKey { get; private set; } = KeyCode.Q;

        [field: Title("Vertical move parameters")]
        [field: SerializeField] 
        public KeyCode JumpActionKey { get; private set; } = KeyCode.Space;
        [field: SerializeField] public KeyCode SlideActionKey { get; private set; } = KeyCode.S;
        
        [field: Title("Horizontal move parameters")]
        [field: SerializeField] public KeyCode MoveLeftActionKey { get; private set; } = KeyCode.A;
        [field: SerializeField] public KeyCode MoveRightActionKey { get; private set; } = KeyCode.D;
        
        [field: Range(0.05f, 1f)]
        [field: SerializeField] 
        public float HorizontalMoveValue { get; private set; } = 0.12f;
        
        [field: Title("Simulation")]
        [field: SerializeField] 
        public KeyCode FreezeThread { get; private set; } = KeyCode.L;        
        
        [field: SerializeField] 
        public KeyCode RestartLevel { get; private set; } = KeyCode.R;
        
        [field: SerializeField]
        public KeyCode KillPlayer { get; private set; } = KeyCode.K;
    }
}
#endif