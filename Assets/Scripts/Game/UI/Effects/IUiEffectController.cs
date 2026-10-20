using Db.Effects;
using Ecs.Utils;

namespace Game.UI.Effects
{
    public interface IUiEffectController
    {
        void PlayBloodEffectAnimation(EUiEffectType effectType);
        void PlaySpeedEffectAnimation(EUiEffectType effectType, ESpeedEffectAxis speedEffectAxis);
        void StopEffects(EUiEffectType effectType);
    }
}