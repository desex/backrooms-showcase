using Db.Effects;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Effects
{
    public partial class UiEffectsView
    {
        [SerializeField] private Image _bloodEffectImage;

        private Tween _bloodEffectAnimation;

        public void PlayBloodEffectAnimation(BloodEffectVo bloodEffectVo)
        {
            // Более показать не могу
        }

        public void StopBloodEffectAnimation(BloodEffectVo bloodEffectVo)
        {
            // Более показать не могу
        }

        private Tween PlayDisappearBloodAnimation(BloodEffectVo bloodEffectVo)
        {
            // Более показать не могу
            return default;
        }
    }
}