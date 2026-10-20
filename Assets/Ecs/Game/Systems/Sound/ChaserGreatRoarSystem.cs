using System;
using Db.Sound;
using Ecs.Game.Components.Sound;
using Ecs.Utils;
using Game.Services.Sound;
using Game.Utils;
using Game.Utils.ColorsUtils;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;
using Random = UnityEngine.Random;

namespace Ecs.Game.Systems.Sound
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 760, nameof(EFeatures.Sound))]
    public class ChaserGreatRoarSystem : ComponentReplacedReactiveSystem<GameEntity, ChaserGreatRoarComponent, bool>
    {
        private readonly GameContext _game;
        private readonly ISoundService _soundService;
        private readonly IChaserSoundParameters _chaserSoundParameters;

        public ChaserGreatRoarSystem(
            GameContext game,
            ISoundService soundService,
            IChaserSoundParameters chaserSoundParameters
        ) : base(game)
        {
            _game = game;
            _soundService = soundService;
            _chaserSoundParameters = chaserSoundParameters;
        }

        protected override Type[] GetComponentTypes()
        {
            // Более показать не могу
            return global::System.Array.Empty<Type>();
        }

        protected override bool GetValue(ChaserGreatRoarComponent component)
        {
            // Более показать не могу
            return default;
        }

        protected override void OnReplaced(GameEntity entity, bool last, bool next)
        {
            // Более показать не могу
        }
    }
}