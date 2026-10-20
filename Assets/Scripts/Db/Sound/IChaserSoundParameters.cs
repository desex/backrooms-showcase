using System.Collections.Generic;
using Db.SoundFxBase;
using UnityEngine;

namespace Db.Sound
{
    public interface IChaserSoundParameters
    {
        Vector2 RoarDelay { get; }
        Vector2 StepVolume { get; }
        float BaseSoundsStartDistance { get; }
        IReadOnlyList<ESoundFxName> RoarSounds { get; }
        ESoundFxName GreatRoarSound { get; }
        float GreatRoarDistance { get; }
    }
}