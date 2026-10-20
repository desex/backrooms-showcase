using System.Collections.Generic;
using Db.SoundFxBase;
using Ecs.Utils;
using Game.Services.Sound;
using Game.Utils.Sound;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;

namespace Ecs.Game.Systems.Sound
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 1000, nameof(EFeatures.Sound))]
    public class RunSoundSystem : ReactiveSystem<GameEntity>
    {
        private readonly ISoundService _soundService;

        public RunSoundSystem(
            GameContext context,
            ISoundService soundService
        ) : base(context)
        {
            _soundService = soundService;
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
    }
}