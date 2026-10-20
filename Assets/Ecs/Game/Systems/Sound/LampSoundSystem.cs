using Db.SoundFxBase;
using Ecs.Core.Interfaces;
using Ecs.Utils;
using Game.Services.Sound;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;
using PdUtils.RandomProvider;

namespace Ecs.Game.Systems.Sound
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 200, nameof(EFeatures.Sound))]
    public class LampSoundSystem : IUpdateSystem
    {
        private readonly GameContext _game;
        private readonly IRandomProvider _randomProvider;
        private readonly ISoundService _soundService;
        private readonly ISoundSettingsBase _soundSettingsBase;
        private readonly ITimeProvider _timeProvider;

        public LampSoundSystem(
            ISoundService soundService,
            ISoundSettingsBase soundSettingsBase,
            GameContext game,
            IRandomProvider randomProvider,
            ITimeProvider timeProvider
        )
        {
            _soundService = soundService;
            _soundSettingsBase = soundSettingsBase;
            _game = game;
            _randomProvider = randomProvider;
            _timeProvider = timeProvider;
        }

        public void Update()
        {
            // Более показать не могу
        }

        private float RandomDelay()
        {
            // Более показать не могу
            return default;
        }
    }
}