using System;
using Db.Effects;
using Ecs.Utils;
using SimpleUi.Abstracts;

namespace Game.UI.Effects
{
    public class UiEffectsController : UiController<UiEffectsView>, IUiEffectController
    {
        private readonly IUiEffectsParameters _uiEffectsParametersBase;
        private ESpeedEffectAxis _speedAxis;

        public UiEffectsController(IUiEffectsParameters uiEffectsParametersBase)
        {
            _uiEffectsParametersBase = uiEffectsParametersBase;
        }
        
        public void PlayBloodEffectAnimation(EUiEffectType effectType)
        {
            // Более показать не могу
        }
        
        public void PlaySpeedEffectAnimation(EUiEffectType effectType, ESpeedEffectAxis speedEffectAxis)
        {
            // Более показать не могу
        }

        public void StopEffects(EUiEffectType effectType)
        {
            // Более показать не могу
        }

        private void PlayEffect(EUiEffectType effectType)
        {
            // Более показать не могу
        }
    }
}