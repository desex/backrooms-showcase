using System;
using Db.SoundFxBase;
using Ecs.Game.Components.Sound;
using Ecs.Utils;
using Game.Services.Sound;
using Game.Utils;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;

namespace Ecs.Game.Systems.Sound
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 750, nameof(EFeatures.Sound))]
    public class ChaserStepSoundSystem : ComponentReplacedReactiveSystem<GameEntity, ChaserBaseSoundsZoneComponent, float>
    {
        private readonly ISoundService _soundService;

        public ChaserStepSoundSystem(
            GameContext game,
            ISoundService soundService
        ) : base(game)
        {
            _soundService = soundService;
        }

        protected override Type[] GetComponentTypes()
        {
            // Более показать не могу
            return global::System.Array.Empty<Type>();
        }

        protected override float GetValue(ChaserBaseSoundsZoneComponent component)
        {
            // Более показать не могу
            return default;
        }

        protected override void OnReplaced(GameEntity entity, float last, float next)
        {
            // Более показать не могу
        }
    }
}