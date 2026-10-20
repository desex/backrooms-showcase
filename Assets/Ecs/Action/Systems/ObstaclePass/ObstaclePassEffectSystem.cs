using System;
using Db.Effects;
using Ecs.Action.Commands.ObstaclePassEffect;
using Ecs.Utils;
using Game.UI.Effects;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems.ObstaclePass
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 621, nameof(EFeatures.UIEffects))]
    public class ObstaclePassEffectSystem : ForEachCommandUpdateSystem<ObstaclePassEffectCommand>
    {
        private readonly GameContext _game;
        private readonly IUiEffectController _uiEffectController;
        private readonly IUiEffectsParameters _uiEffectsParameters;

        public ObstaclePassEffectSystem(
            ICommandBuffer commandBuffer,
            IUiEffectController uiEffectController,
            GameContext game,
            IUiEffectsParameters uiEffectsParameters
        ) : base(commandBuffer)
        {
            _uiEffectController = uiEffectController;
            _game = game;
            _uiEffectsParameters = uiEffectsParameters;
        }

        protected override void Execute(ref ObstaclePassEffectCommand command)
        {
            if (command.SpeedEffectAxis == ESpeedEffectAxis.None)
                throw new ArgumentException();

            if (_game.TutorialEntity.IsInTutorial)
                return;

            var speedEffectDictionary = _uiEffectsParameters.SpeedEffects;

            if (!speedEffectDictionary.TryGetValue(command.SpeedEffectAxis, out var speedEffectParameters))
                return;

            _game.PlayerEntity.ReplaceObstacleFeedbackAfterTimer(speedEffectParameters.DurationTimer);
            _uiEffectController.PlaySpeedEffectAnimation(EUiEffectType.SpeedEffect, command.SpeedEffectAxis);
        }
    }
}