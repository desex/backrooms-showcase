using Db.Player;
using Ecs.Action.Commands.Movement;
using Ecs.Utils;
using Game.Services.Player;
using Game.Services.Render;
using Game.Services.Score;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems.Movement
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 804, nameof(EFeatures.Move))]
    public class StartAccelerationSystem : ForEachCommandUpdateSystem<StartAccelerationCommand>
    {
        private readonly IAccelerationFactorService _accelerationFactorService;
        private readonly GameContext _game;
        private readonly IPlayerAccelerationParametersBase _playerAccelerationParametersBase;
        private readonly IScoreService _scoreService;
        private readonly ISpeedEffectService _speedEffectService;

        public StartAccelerationSystem(
            GameContext game,
            IPlayerAccelerationParametersBase playerAccelerationParametersBase,
            ICommandBuffer commandBuffer,
            ISpeedEffectService speedEffectService,
            IScoreService scoreService,
            IAccelerationFactorService accelerationFactorService
        ) : base(commandBuffer)
        {
            _game = game;
            _playerAccelerationParametersBase = playerAccelerationParametersBase;
            _speedEffectService = speedEffectService;
            _scoreService = scoreService;
            _accelerationFactorService = accelerationFactorService;
        }

        protected override void Execute(ref StartAccelerationCommand command)
        {
            var tutorialEntity = _game.TutorialEntity;
            var isTutorialActive = tutorialEntity.IsInTutorial;

            if (isTutorialActive)
                return;

            if (!_scoreService.HasCombo.CurrentValue)
                return;

            var player = _game.PlayerEntity;

            if (player.IsDash || player.IsDamaged)
                return;

            var accelerationDuration = _playerAccelerationParametersBase.AccelerationDuration;
            player.ReplaceSpeedAcceleration(accelerationDuration);
            _speedEffectService.EnableSpeedEffect(command.ObstacleTypeForAcceleration);
            _accelerationFactorService.InterpolateAccelerationFactor();
            player.IsFastActionMove = false;
        }
    }
}