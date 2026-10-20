#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
using UnityEngine;

namespace Db.PlayerInput
{
    public interface IKeyboardInputParameters
    {
        bool IsEnabled { get; }
        
        KeyCode DashActionKey { get; }
        
        KeyCode JumpActionKey { get; }        
        KeyCode SlideActionKey { get; }    
        
        KeyCode MoveLeftActionKey { get; }        
        KeyCode MoveRightActionKey { get; }
        float HorizontalMoveValue { get; }
        KeyCode FreezeThread { get; }
        KeyCode RestartLevel { get; }
        KeyCode KillPlayer { get; }
    }
}
#endif