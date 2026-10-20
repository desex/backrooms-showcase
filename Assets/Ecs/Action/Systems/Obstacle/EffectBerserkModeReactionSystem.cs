using Db.Obstacles;
using Db.Player;
using Db.SoundFxBase;
using Ecs.Action.Commands.Obstacles;
using Ecs.Utils;
using Game.Services.Effects;
using Game.Services.General;
using Game.Services.Sound;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;
using UnityEngine;

namespace Ecs.Action.Systems.Obstacle
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 590, nameof(EFeatures.Move))]
    public class EffectBerserkModeReactionSystem : ForEachCommandUpdateSystem<PlayBerserkModeReactionEffectCommand>
    {
        private readonly IDashParameters _dashParameters;
        private readonly IPoolEffectsService _effectsService;
        private readonly GameContext _game;
        private readonly IReactionObstacleEffectBase _obstacleEffect;
        private readonly ISoundService _soundService;

        public EffectBerserkModeReactionSystem(
            ICommandBuffer commandBuffer,
            IPoolEffectsService effectsService,
            ISoundService soundService,
            IReactionObstacleEffectBase obstacleEffect,
            GameContext game,
            IDashParameters dashParameters
        ) : base(commandBuffer)
        {
            _effectsService = effectsService;
            _soundService = soundService;
            _obstacleEffect = obstacleEffect;
            _game = game;
            _dashParameters = dashParameters;
        }

        protected override void Execute(ref PlayBerserkModeReactionEffectCommand effectCommand)
        {
            // Более показать не могу
        }
    }
}