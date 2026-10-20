using System;
using Db.Player;
using Db.Render.SpeedShader;
using Ecs.Game.Components.Movement;
using Ecs.Utils;
using Game.Services.Render;
using Game.Utils.Movement;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;
using UnityEngine;

namespace Ecs.Game.Systems.Shader.SpeedShader
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 940, nameof(EFeatures.Common))]
    public class SpeedShaderAccelerationFeedbackSystem : ComponentReplacedReactiveSystem<GameEntity, ForwardSpeedComponent, ForwardSpeed>
    {
        private readonly IPlayerBasicMovementParametersBase _playerBasicMovementParametersBase;
        private readonly IPlayerSpeedProgressionParameters _playerSpeedProgressionParameters;
        private readonly IBorderedSpeedShaderParameters _borderedSpeedShaderParameters;
        private readonly ISpeedEffectService _speedEffectService;

        public SpeedShaderAccelerationFeedbackSystem(
            IPlayerBasicMovementParametersBase playerBasicMovementParametersBase,
            IPlayerSpeedProgressionParameters playerSpeedProgressionParameters,
            IBorderedSpeedShaderParameters borderedSpeedShaderParameters,
            GameContext game,
            ISpeedEffectService speedEffectService
        ) : base(game)
        {
            _playerBasicMovementParametersBase = playerBasicMovementParametersBase;
            _playerSpeedProgressionParameters = playerSpeedProgressionParameters;
            _borderedSpeedShaderParameters = borderedSpeedShaderParameters;
            _speedEffectService = speedEffectService;
        }

        protected override Type[] GetComponentTypes() 
            => GameComponentsLookup.ComponentTypes;

        protected override ForwardSpeed GetValue(ForwardSpeedComponent component)
            => component?.Value ?? new ForwardSpeed(0f,0f);

        protected override void OnReplaced(GameEntity entity, ForwardSpeed last, ForwardSpeed next)
        {
            if (!entity.IsPlayer)
                return;

            var isInJump = entity.HasJumping && entity.Jumping.Value is EJumpType.DoubleJump or EJumpType.Jump;
            if (isInJump)
                return;
            
            var initialForwardSpeed = _playerBasicMovementParametersBase.Speed;
            var maxForwardSpeed = _playerSpeedProgressionParameters.MaxProgressionMultiply * initialForwardSpeed;
            var shaderSide = _speedEffectService.GetSpeedEffectAxis();
            var increasePercent = CalculateIncreasePercent();
            
            var initialSpeed = _borderedSpeedShaderParameters.InitialSpeed;
            var initialMaskSize = _borderedSpeedShaderParameters.InitialMaskSize;
            var initialMaskContrast = _borderedSpeedShaderParameters.InitialMaskContrast;
            var initialSmoothness = _borderedSpeedShaderParameters.InitialSmoothness;
            var initialPower = _borderedSpeedShaderParameters.InitialPower;

            var targetSpeed = 0f;
            var targetMaskSize = 0f;
            var targetMaskContrast = 0f;
            var targetSmoothness = 0f;
            var targetPower = 0f;
            AssignTargetParameters();
            
            var speed = Mathf.Lerp(initialSpeed, targetSpeed, increasePercent);
            var maskSize = Mathf.Lerp(initialMaskSize, targetMaskSize, increasePercent);
            var maskContrast = Mathf.Lerp(initialMaskContrast, targetMaskContrast, increasePercent);
            var smoothness = Mathf.Lerp(initialSmoothness, targetSmoothness, increasePercent);
            var power = Mathf.Lerp(initialPower, targetPower, increasePercent);
            
            _speedEffectService.ChangeParametersInSpeedBoostFeature(speed, maskSize, maskContrast, smoothness, power);
            
            return;

            float CalculateIncreasePercent()
            {
                var currentClampedSpeed = Mathf.Clamp(last.Current, initialForwardSpeed, maxForwardSpeed);
                var speedIncrease = currentClampedSpeed - initialForwardSpeed;
                var range = maxForwardSpeed - initialForwardSpeed;
                return speedIncrease / range;
            }

            void AssignTargetParameters()
            {
                switch (shaderSide)
                {
                    case ESpeedEffectAxis.All:
                    case ESpeedEffectAxis.None:
                        targetSpeed = _borderedSpeedShaderParameters.TargetAllSideSpeed;
                        targetMaskSize = _borderedSpeedShaderParameters.TargetAllSideMaskSize;
                        targetMaskContrast = _borderedSpeedShaderParameters.TargetAllSideMaskContrast;
                        targetSmoothness = _borderedSpeedShaderParameters.TargetAllSideSmoothness;
                        targetPower = _borderedSpeedShaderParameters.TargetAllSidePower;
                        break;
                    case ESpeedEffectAxis.Down:
                    case ESpeedEffectAxis.Left:
                    case ESpeedEffectAxis.Right:
                    case ESpeedEffectAxis.LeftAndRight:
                    case ESpeedEffectAxis.Up:
                        targetSpeed = _borderedSpeedShaderParameters.TargetBorderSideSpeed;
                        targetMaskSize = _borderedSpeedShaderParameters.TargetBorderSideMaskSize;
                        targetMaskContrast = _borderedSpeedShaderParameters.TargetBorderSideMaskContrast;
                        targetSmoothness = _borderedSpeedShaderParameters.TargetBorderSideSmoothness;
                        targetPower = _borderedSpeedShaderParameters.TargetBorderSidePower;
                        break;
                }
            }
        }
    }
}