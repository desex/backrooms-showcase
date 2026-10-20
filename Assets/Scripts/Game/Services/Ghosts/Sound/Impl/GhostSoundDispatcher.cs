using System.Collections.Generic;
using Db.SoundFxBase;
using Ecs.Managers;
using Game.Services.Settings;
using Game.Services.Sound;
using Game.Services.Sound.Impl;
using UnityEngine;

namespace Game.Services.Ghosts.Sound.Impl
{
    public class GhostSoundDispatcher : IGhostSoundDispatcher
    {
        private readonly GameContext _game;
        private readonly ISettingsService _settingsService;
        private readonly ISoundFxBase _soundFxBase;
        private readonly Dictionary<Uid, ISoundService> _soundServices = new Dictionary<Uid, ISoundService>();

        public GhostSoundDispatcher(ISoundFxBase soundFxBase, ISettingsService settingsService, GameContext game)
        {
            _settingsService = settingsService;
            _game = game;
            _soundFxBase = soundFxBase;
        }

        public void AddGhostSoundService(Uid ghostUid)
        {
            // Более показать не могу
        }

        public void PlaySound(Uid targetGhostUid, ESoundFxName soundFxName)
        {
            // Более показать не могу
        }

        public void StopSound(Uid targetGhostUid, ESoundFxName soundFxName)
        {
            // Более показать не могу
        }

        public void StopCharacterEffectSounds(Uid targetGhostUid)
        {
            // Более показать не могу
        }

        public void StopMoveActionSounds(Uid targetGhostUid)
        {
            // Более показать не могу
        }
    }
}