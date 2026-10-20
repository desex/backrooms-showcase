using Db.Effects;
using DG.Tweening;
using Ecs.Utils;
using Game.Utils.SerializeCollection.Impl.Dictionary;
using UnityEngine;

namespace Game.UI.Effects
{
    public partial class UiEffectsView
    {
        [SerializeField] private DictionarySpeedEffectImages _speedEffectImages;

        private CanvasGroup _speedEffectImage;
        private Tween _speedEffectAnimation;
        
        public void SetAxisSpeedEffect(ESpeedEffectAxis axis) {
            // Более показать не могу
        }
        
        public void PlaySpeedEffectAnimation(SpeedEffectVo speedEffectVo)    
        {
            // Более показать не могу
        }

        public void StopSpeedEffectAnimation(SpeedEffectVo speedEffectVo)
        {
            // Более показать не могу
        }
        
        private Tween PlayDisappearSpeedAnimation(SpeedEffectVo speedEffectVo)
        {
            // Более показать не могу
            return default;
        }
    }
}