using System;
using Db.Render.SpeedShader;
using Ecs.Game.Components.Movement;
using Ecs.Utils;
using Game.Services.Render;
using Game.Utils.Movement;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;

namespace Ecs.Game.Systems.Shader.SpeedShader
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 940, nameof(EFeatures.Common))]
    public class SpeedShaderEnableSystem
        : ComponentReplacedReactiveSystem<GameEntity, ForwardSpeedComponent, ForwardSpeed>
    {
        private readonly IBorderedSpeedShaderParameters _borderedSpeedShaderParameters;
        private readonly ISpeedEffectService _speedRenderFeature;

        public SpeedShaderEnableSystem(
            IBorderedSpeedShaderParameters borderedSpeedShaderParameters,
            GameContext game,
            ISpeedEffectService speedRenderFeature
        ) : base(game)
        {
            _borderedSpeedShaderParameters = borderedSpeedShaderParameters;
            _speedRenderFeature = speedRenderFeature;
        }

        protected override Type[] GetComponentTypes()
        {
            return GameComponentsLookup.ComponentTypes;
        }

        protected override ForwardSpeed GetValue(ForwardSpeedComponent component)
        {
            return component?.Value ?? new ForwardSpeed(0f, 0f);
        }

        protected override void OnReplaced(GameEntity player, ForwardSpeed last, ForwardSpeed next)
        {
            if (!player.IsPlayer)
                return;

            var currentSpeed = last.Current;
            var shaderActivationSpeed = _borderedSpeedShaderParameters.ShaderActivationSpeed;

            var notInJump = !player.HasJumping || player.Jumping.Value is not (EJumpType.DoubleJump or EJumpType.Jump);

            if (currentSpeed <= shaderActivationSpeed && !player.IsDash && !player.IsBerserkMode)
                _speedRenderFeature.ChangeActiveFeature(false);
            else if(player.HasQteType)
                _speedRenderFeature.ChangeActiveFeature(false);
            else if (notInJump)
                _speedRenderFeature.ChangeActiveFeature(true);
        }
    }
}