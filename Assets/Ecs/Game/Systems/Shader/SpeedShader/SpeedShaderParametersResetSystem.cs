using Db.Render.SpeedShader;
using Ecs.Action.Commands.Shader;
using Ecs.Utils;
using Game.Services.Render;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Game.Systems.Shader.SpeedShader
{
    [Install(ExecutionType.Game, ExecutionPriority.High, 50, nameof(EFeatures.Initialization))]
    public class SpeedShaderParametersResetSystem : ForEachCommandUpdateSystem<ResetSpeedShaderCommand>
    {
        private readonly IBorderedSpeedShaderParameters _borderedSpeedShaderParameters;
        private readonly ISpeedEffectService _speedEffectService;

        public SpeedShaderParametersResetSystem(
            ICommandBuffer commandBuffer,
            IBorderedSpeedShaderParameters borderedSpeedShaderParameters,
            ISpeedEffectService speedEffectService
        ) : base(commandBuffer)
        {
            _borderedSpeedShaderParameters = borderedSpeedShaderParameters;
            _speedEffectService = speedEffectService;
        }

        protected override void Execute(ref ResetSpeedShaderCommand command)
        {
            var initialSpeed = _borderedSpeedShaderParameters.InitialSpeed;
            var initialMaskSize = _borderedSpeedShaderParameters.InitialMaskSize;
            var initialMaskContrast = _borderedSpeedShaderParameters.InitialMaskContrast;
            var initialSmoothness = _borderedSpeedShaderParameters.InitialSmoothness;
            var initialPower = _borderedSpeedShaderParameters.InitialPower;
            
            _speedEffectService.ChangeParametersInSpeedBoostFeature(initialSpeed, 
                                                                      initialMaskSize,
                                                                      initialMaskContrast, 
                                                                      initialSmoothness,
                                                                      initialPower
            );
        }
    }
}