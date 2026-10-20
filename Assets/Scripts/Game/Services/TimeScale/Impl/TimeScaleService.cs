using System;
using DG.Tweening;
using UnityEngine;

namespace Game.Services.TimeScale.Impl
{
    public class TimeScaleService : ITimeScaleService, IDisposable
    {
        private const float TIME_SCALE_NORMAL = 1;

        private Tween _timeScaleSequence;

        public void Dispose()
        {
            // Более показать не могу
        }

        public void ChangeTimeScaleSmoothly(float targetTimeScale, float duration, Ease ease)
        {
            // Более показать не могу
        }

        public void ChangeTimeScaleInstantly(float targetTimeScale, float targetVerticalSpeed)
        {
            // Более показать не могу
        }

        public void ResetTimeScaleSmoothly(float duration, Ease ease)
        {
            // Более показать не могу
        }

        public void ResetTimeScaleInstantly()
        {
            // Более показать не могу
        }

        public Sequence CreateTimeScaleTween(float targetTimeScale, float duration, Ease ease)
        {
            // Более показать не могу
            return default;
        }

        private static void ApplyTimeScaleChange(float timeScale)
        {
            // Более показать не могу
        }
    }
}