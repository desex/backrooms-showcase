using System;
using DG.Tweening;

namespace Db.UI.Checkpoint.Impl
{
    [Serializable]
    public struct CheckpointUiAnimationVo
    {
        public float TimeToAppearView;
        public float TimeToShowView;
        public float TimeToDisappearView;
        public Ease AppearEase;
        public Ease DisappearEase;
        public float AlphaHideValue;
        public float AlphaShowValue;
    }
}