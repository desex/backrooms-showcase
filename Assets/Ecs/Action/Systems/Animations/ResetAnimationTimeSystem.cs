using Ecs.Action.Commands.Animations;
using Ecs.Utils;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems.Animations
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 300, nameof(EFeatures.Animation))]
    public class ResetAnimationTimeSystem : ForEachCommandUpdateSystem<ResetAnimationTimeCommand>
    {
        private readonly GameContext _game;
        

        public ResetAnimationTimeSystem(
            ICommandBuffer commandBuffer,
            GameContext game
        ) : base(commandBuffer)
        {
            _game = game;
        }

        protected override void Execute(ref ResetAnimationTimeCommand command)
        {
            var playerAnimancer = _game.PlayerEntity.Animancer.Value;
            var layer = playerAnimancer.Layers[command.Layer];

            for (var i = 0; i < layer.ChildCount; i++)
            {
                var state = layer.GetChild(i);
                
                if (state == null)
                    continue;

                state.Time = 0f;
            }
        }
    }
}