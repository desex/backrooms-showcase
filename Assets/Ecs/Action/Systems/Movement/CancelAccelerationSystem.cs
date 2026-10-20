using Db.Player;
using Ecs.Action.Commands.Movement;
using Ecs.Utils;
using Game.Services.Player;
using Game.Services.Render;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems.Movement
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 805, nameof(EFeatures.Move))]
    public class CancelAccelerationSystem : ForEachCommandUpdateSystem<CancelAccelerationCommand>
    {
        private readonly IAccelerationFactorService _accelerationFactorService;
        private readonly GameContext _game;
        private readonly IPlayerAccelerationParametersBase _playerAccelerationParametersBase;
        private readonly ISpeedEffectService _speedEffectService;

        public CancelAccelerationSystem(
            ICommandBuffer commandBuffer,
            GameContext game,
            ISpeedEffectService speedEffectService,
            IPlayerAccelerationParametersBase playerAccelerationParametersBase,
            IAccelerationFactorService accelerationFactorService
        ) : base(commandBuffer)
        {
            _game = game;
            _speedEffectService = speedEffectService;
            _playerAccelerationParametersBase = playerAccelerationParametersBase;
            _accelerationFactorService = accelerationFactorService;
        }

        protected override void Execute(ref CancelAccelerationCommand command)
        {
            var character = _game.GetEntityWithUid(command.CharacterUid);

            if (character.IsPlayer)
                _speedEffectService.DisableSpeedEffect();

            if (character.HasSpeedAcceleration)
                character.ReplaceSpeedAcceleration(0f);

            _accelerationFactorService.SetAccelerationFactor(_playerAccelerationParametersBase.AccelerationFactor);

            character.IsSpeedBoost = false;
        }
    }
}