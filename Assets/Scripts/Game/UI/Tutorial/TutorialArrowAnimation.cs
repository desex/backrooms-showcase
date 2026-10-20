using System;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Tutorial
{
    [Serializable]
    public class TutorialArrowAnimation
    {
        [Title("Animation targets")]
        [SerializeField] private List<Image> _arrowSegments;

        [Title("Animation parameters")]
        [SerializeField] private float _maxSegmentScale = 1.08f;
        [SerializeField] private float _minSegmentAlpha = 0.2f;
        [SerializeField] private float _segmentFadeDuration = 0.25f;
        [SerializeField] private float _segmentDelay = 0.09f;
        [SerializeField] private float _delayBetweenWaves = 0.12f;
        [SerializeField] private Ease _ease = Ease.InOutSine;

        private Sequence _sequence;

        public void StartAnimation()
        {
            // Более показать не могу
        }
        
        public void ResetAnimation()
        {
            // Более показать не могу
        }
    }
}