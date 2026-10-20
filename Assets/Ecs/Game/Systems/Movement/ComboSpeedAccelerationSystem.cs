using Db.Player;
using Ecs.Action.Commands.Movement;
using Ecs.Utils;
using Game.Services.Player;
using Game.Services.Render;
using Game.Services.Score;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Game.Systems.Movement
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 500, nameof(EFeatures.Move))]
    public class ComboSpeedAccelerationSystem : ForEachCommandUpdateSystem<ComboAccelerateCommand>
    {
        private readonly IAccelerationFactorService _accelerationFactorService;
        private readonly GameContext _game;
        private readonly IPlayerAccelerationParametersBase _playerAccelerationParametersBase;
        private readonly IScoreService _scoreService;
        private readonly ISpeedEffectService _speedEffectService;

        public ComboSpeedAccelerationSystem(
            ICommandBuffer commandBuffer,
            IPlayerAccelerationParametersBase playerAccelerationParametersBase,
            ISpeedEffectService speedEffectService,
            GameContext game,
            IAccelerationFactorService accelerationFactorService,
            IScoreService scoreService
        ) : base(commandBuffer)
        {
            _playerAccelerationParametersBase = playerAccelerationParametersBase;
            _speedEffectService = speedEffectService;
            _game = game;
            _accelerationFactorService = accelerationFactorService;
            _scoreService = scoreService;
        }

        protected override void Execute(ref ComboAccelerateCommand command)
        {
            if (!_scoreService.HasCombo.CurrentValue)
                return;

            var character = _game.PlayerEntity;
            var entityUid = command.CharacterUid;

            ApplySpeedBoost(character);

            var entityToAccelerate = _game.GetEntityWithUid(entityUid);
            var accelerationDuration = _playerAccelerationParametersBase.AccelerationDuration;
            entityToAccelerate.ReplaceSpeedAcceleration(accelerationDuration);

            if (entityToAccelerate.IsPlayer)
                _speedEffectService.EnableSpeedEffect();
        }

        private void ApplySpeedBoost(GameEntity character)
        {
            if (!character.HasReadyToObstaclePass
                && !character.HasSliding
                && !character.IsControllableSlideInterrupted
                && !character.IsReadyToFailFall
                && !character.HasSpeedAcceleration)
            {
                character.IsSpeedBoost = true;

                _accelerationFactorService.InterpolateAccelerationFactor();
            }
        }
    }
}