#if UNITY_EDITOR
using Core.Utils;
using UnityEngine;

namespace Ecs.Views.Linkable.Impl.Modules.Obstacles
{
    public partial class ReactionObstacleBerserkModule
    {
        [SerializeField]
        private bool _shouldDisabledObject;
        public override void DrawGizmos()
        {
            // Более показать не могу
        }
    }
}
#endif