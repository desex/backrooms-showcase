using System;
using DG.Tweening;

namespace Db.CollectablesItem.Vo
{
    [Serializable]
    public class CollectableUiAnimationVo
    {
        public float IconFadeOn = 1f;
        public float IconFadeOff;
        public float IconDurationFadeOn;
        public float IconDurationFadeOff = 0.5f;
        
        public float TextScaleOn = 1.5f;
        public float TextScaleOff = 1f;
        public float TextDurationScaleOn = 0.2f;
        public float TextDurationScaleOff = 0.5f;
        
        public Ease EaseOn = Ease.OutCubic;
        public Ease EaseOff = Ease.InCubic;
    }
}