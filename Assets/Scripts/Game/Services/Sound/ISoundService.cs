using System;
using Db.SoundFxBase;
using FMODUnity;
using UnityEngine;

namespace Game.Services.Sound
{
    public interface ISoundService
    {
        void PlaySound(ESoundFxName soundFxName, Transform attachedTransform = null);
        void PlaySound(ESoundFxName soundFxName, Action onSoundFinished, Transform attachedTransform = null);
        void PlayingAndChangingSoundParameters(ESoundFxName soundFxName, ESoundParameter parameter, int key);
        StudioEventEmitter GetEmitter(ESoundFxName soundFxName);
        void PauseSound(ESoundFxName soundFxName, bool isPause = true);
        void PauseAllSounds(bool isPause = true);
        void StopCharacterEffectSounds();
        void StopMoveActionSounds();
        public void MuteSounds(ESoundFxName sound, bool isMute);
        void StopSound(ESoundFxName soundFxName);
        void StopAllSounds();
        void ChangeVolume(ESoundFxName soundFxName, float volume);
    }
}