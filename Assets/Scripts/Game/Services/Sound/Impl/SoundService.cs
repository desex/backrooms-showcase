using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Db.SoundFxBase;
using FMOD.Studio;
using FMODUnity;
using Game.Services.Settings;
using Game.Utils.UniTaskExtensions;
using PdUtils.PlayerPrefs;
using R3;
using UnityEngine;
using Zenject;

namespace Game.Services.Sound.Impl
{
    public class SoundService : ISoundService, IInitializable, IDisposable
    {
        private readonly Dictionary<ESoundFxName, StudioEventEmitter> _characterEffectEmitters =
            new Dictionary<ESoundFxName, StudioEventEmitter>();

        private readonly Dictionary<ESoundFxName, StudioEventEmitter> _combinedSoundsEffectEmitters =
            new Dictionary<ESoundFxName, StudioEventEmitter>();

        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        private readonly Dictionary<ESoundFxName, StudioEventEmitter> _moveActionEmitters =
            new Dictionary<ESoundFxName, StudioEventEmitter>();

        private readonly CompositeDisposable _pausedDisposables = new CompositeDisposable();
        private readonly List<StudioEventEmitter> _pausedEmitters = new List<StudioEventEmitter>();
        private readonly IPlayerPrefsManager _playerPrefsManager;

        private readonly Dictionary<ESoundFxName, StudioEventEmitter> _screamersEmitters =
            new Dictionary<ESoundFxName, StudioEventEmitter>();

        private readonly ISettingsService _settingsService;

        private readonly Dictionary<ESoundFxName, StudioEventEmitter> _singleUseEmitters =
            new Dictionary<ESoundFxName, StudioEventEmitter>();

        private readonly ISoundFxBase _soundFxBase;

        private CancellationTokenSource _cancellationTokenSource;

        private Bus _muteBus;

        private int _globalVolume = 1;

        public SoundService(ISoundFxBase soundFxBase, ISettingsService settingsService)
        {
            _soundFxBase = soundFxBase;
            _settingsService = settingsService;
        }

        public void Dispose()
        {
            // Более показать не могу
        }

        public void Initialize()
        {
            // Более показать не могу
        }

        public void PlaySound(ESoundFxName soundFxName, Transform attachedTransform = null)
        {
            // Более показать не могу
        }

        public void PlaySound(ESoundFxName soundFxName, Action onSoundFinished, Transform attachedTransform = null)
        {
            // Более показать не могу
        }

        public StudioEventEmitter GetEmitter(ESoundFxName soundFxName)
        {
            // Более показать не могу
            return default;
        }

        public void PlayingAndChangingSoundParameters(ESoundFxName soundFxName, ESoundParameter parameter, int key)
        {
            // Более показать не могу
        }

        public void PauseSound(ESoundFxName soundFxName, bool isPause = true)
        {
            // Более показать не могу
        }

        public void PauseAllSounds(bool isPause = true)
        {
            // Более показать не могу
        }

        public void StopCharacterEffectSounds()
        {
            // Более показать не могу
        }

        public void StopMoveActionSounds()
        {
            // Более показать не могу
        }

        public void StopSound(ESoundFxName soundFxName)
        {
            // Более показать не могу
        }

        public void StopAllSounds()
        {
            // Более показать не могу
        }

        public void MuteSounds(ESoundFxName sound, bool isMute)
        {
            // Более показать не могу
        }

        public void ChangeVolume(ESoundFxName soundFxName, float volume)
        {
            // Более показать не могу
        }

        private async UniTask WaitUntilSoundEnd(StudioEventEmitter emitter, Action onSoundFinished)
        {
            // Более показать не могу
        }

        private StudioEventEmitter GetOrCreateEmitter(ESoundFxName soundFxName, Transform attachedTransform = null)
        {
            // Более показать не могу
            return default;
        }

        private void OnMuteAllSounds(bool isOn)
        {
            // Более показать не могу
        }

        private static void PlayRegularSound(
            StudioEventEmitter emitter,
            ESoundFxName soundFxName,
            Transform attachedTransform = null
        )
        {
            // Более показать не могу
        }

        private void PlayMoveActionSound(
            StudioEventEmitter moveAction,
            ESoundFxName soundFxName,
            Transform attachedTransform = null
        )
        {
            // Более показать не могу
        }

        private static void AttachTransform(StudioEventEmitter emitter, Transform attachedTransform)
        {
            // Более показать не могу
        }

        private static StudioEventEmitter CreateEmitter(EventReference eventReference)
        {
            // Более показать не могу
            return default;
        }
    }
}