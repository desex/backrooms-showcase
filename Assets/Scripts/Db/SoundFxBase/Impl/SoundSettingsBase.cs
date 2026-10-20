using Sirenix.OdinInspector;
using UnityEngine;

namespace Db.SoundFxBase.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(SoundSettingsBase), fileName = nameof(SoundSettingsBase))]
    public class SoundSettingsBase : ScriptableObject, ISoundSettingsBase
    {
        [field: MinMaxSlider(0, 30, true)]
        [field: SerializeField] 
        public Vector2 SecondsBetweenLampSound { get; private set; }

        [field: SerializeField] public float DelayBeforeFallSoundWhenDead { get; private set; }
        
        [field: SerializeField] public float DelayBeforeSomersaultSoundWhenForwardJump { get; private set; }
    }
}