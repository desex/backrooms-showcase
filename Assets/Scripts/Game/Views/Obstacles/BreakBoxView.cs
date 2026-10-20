using Core.Utils;
using Core.Utils.TriggerCollision;
using Game.Utils.Obstacles;
using UnityEngine;

namespace Game.Views.Obstacles
{
    public class BreakBoxView : MonoBehaviour
    {
        public Box BoxTrigger;
        public Box BoxTriggerNotAnimatedBox;

        [field: SerializeField]
        public EBreakBoxType Type { get; private set; }

        [field: SerializeField]
        public BreakBoxData BreakBoxData { get; private set; }

#if UNITY_EDITOR
        protected void OnDrawGizmosSelected()
        {
            // Более показать не могу
        }
#endif
    }
}