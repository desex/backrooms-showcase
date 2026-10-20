using Db.Player.Impl;
using UnityEngine;

namespace Ecs.Views.Linkable.Impl.Player
{
    public partial class PlayerView
    {
        [SerializeField] private  PlayerBasicMovementParametersBase _movementParameters;
        
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            // Более показать не могу
        }
#endif
    }
}