using System.Collections.Generic;
using Db.SoundScreamer;
using Ecs.Utils;
using Game.Services.Analytics;
using Game.Services.Sound;
using Game.Utils;
using Game.Utils.Sound;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;
using PdUtils.RandomProvider;

namespace Ecs.Game.Systems.Screamers
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 1000, nameof(EFeatures.Sound))]
    public class ScreamerPlayBackSystem : ReactiveSystem<GameEntity>
    {
        private readonly IAnalyticsService _analyticsService;
        private readonly IRandomProvider _randomProvider;
        private readonly IScreamerParametersBase _screamerParameters;
        private readonly ISoundService _soundService;

        public ScreamerPlayBackSystem(
            IScreamerParametersBase screamerParameters,
            ISoundService soundService,
            IRandomProvider randomProvider,
            GameContext gameContext,
            IAnalyticsService analyticsService
        ) : base(gameContext)
        {
            _screamerParameters = screamerParameters;
            _soundService = soundService;
            _randomProvider = randomProvider;
            _analyticsService = analyticsService;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            // Более показать не могу
            return default;
        }

        protected override bool Filter(GameEntity entity)
        {
            // Более показать не могу
            return default;
        }

        protected override void Execute(IEnumerable<GameEntity> entities)
        {
            // Более показать не могу
        }

        private void SetNextScreamer(GameEntity entity)
        {
            // Более показать не могу
        }

        private ScreamerParameterVo GetRandomScreamerParameters()
        {
            // Более показать не могу
            return default;
        }

        private float RandomiseTimeRangePlay(ScreamerParameterVo screamerParameters)
        {
            // Более показать не могу
            return default;
        }

        private void PlaySoundScreamer(GameEntity player)
        {
            // Более показать не могу
        }

        private void OnScreamerSoundEnd(GameEntity entity)
        {
            // Более показать не могу
        }
    }
}