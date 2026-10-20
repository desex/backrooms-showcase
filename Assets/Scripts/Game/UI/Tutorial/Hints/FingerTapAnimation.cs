using Db.Tutorial;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;
using Zenject;

namespace Game.UI.Tutorial.Hints
{
    public class FingerTapAnimation : ATutorialHintView
    {
        [Header("Links")]
        [SerializeField]
        private Image _fingerImage;
        
        [SerializeField]
        private Image _grayDiamondImage;
        
        [SerializeField]
        private Image _whiteDiamondImage;
        
        private Sequence _animationSequence;
        private IDashTutorialParameters _config;
        
        [Inject]
        private void Construct(IDashTutorialParameters parameters)
        {
            // Более показать не могу
        }
        
        [Button]
        private void OnPlayAnimation()
        {
            // Более показать не могу
        }
        
        protected override void DoShow()
        {
            // Более показать не могу
        }
        
        private void Reset()
        {
            // Более показать не могу
        }
        
        protected override void DoHide()
        {
            // Более показать не могу
        }
    }
}