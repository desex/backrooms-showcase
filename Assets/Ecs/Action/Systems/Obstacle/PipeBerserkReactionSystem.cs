using Ecs.Action.Commands.Obstacles;
using Ecs.Utils;
using Game.Utils.Obstacles;
using Generated.Commands;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;
using UnityEngine;

namespace Ecs.Action.Systems.Obstacle
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 590, nameof(EFeatures.Move))]
    public class PipeBerserkReactionSystem : ForEachCommandUpdateSystem<PipesDestroyCommand>
    {
        private static readonly int _arrayIndex = Shader.PropertyToID("_Array_Index");
        private readonly ICommandBuffer _commandBuffer;

        public PipeBerserkReactionSystem(ICommandBuffer commandBuffer) : base(commandBuffer)
        {
            _commandBuffer = commandBuffer;
        }

        protected override void Execute(ref PipesDestroyCommand command)
        {
            var colorPicker = command.ColorPicker;
            var color = colorPicker.GetFloat(_arrayIndex);
            var effectType = EObstacleEffectType.PipeBlue;

            if (Mathf.Approximately(color, 1f))
                effectType = EObstacleEffectType.PipeGreen;
            else if (Mathf.Approximately(color, 2f))
                effectType = EObstacleEffectType.PipeRed;
            else if (Mathf.Approximately(color, 3f))
                effectType = EObstacleEffectType.PipeYellow;

            _commandBuffer.PlayBerserkModeReactionEffect(effectType);
        }
    }
}