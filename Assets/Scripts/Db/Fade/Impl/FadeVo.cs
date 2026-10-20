using System;
using DG.Tweening;
using UnityEngine;

namespace Db.Fade.Impl
{
    [Serializable]
    public struct FadeVo
    {
        [field: Space]
        [field: Header("Fade Parameters")]
        public EFadeType Type;

        public float DelayBeforeShow;
        public float FadeInDuration;
        public float FadeBlackDuration;
        public float FadeOutDuration;
        public float FadeDelay;

        public Ease Ease;
    }
}