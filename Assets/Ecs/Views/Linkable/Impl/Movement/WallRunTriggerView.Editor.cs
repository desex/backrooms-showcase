#if UNITY_EDITOR
using Core.Utils;
using UnityEngine;

namespace Ecs.Views.Linkable.Impl.Movement
{
    public partial class WallRunTriggerView
    {
        private void OnDrawGizmosSelected()
        {
            BoxGizmosUtil.Draw(Color.green, transform, _trigger);
        }
    }
}
#endif