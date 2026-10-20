using UnityEngine;

namespace Db.SoundFxBase
{
    public interface ISoundSettingsBase
    {
        Vector2 SecondsBetweenLampSound { get; }
        float DelayBeforeFallSoundWhenDead { get; }
        float DelayBeforeSomersaultSoundWhenForwardJump { get; }
    }
}