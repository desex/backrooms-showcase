#if !DISABLE_GHOSTS
using Ecs.Action.Commands.Obstacles;
using Ecs.Utils;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems.Ghost
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 941, nameof(EFeatures.Ghost))]
    public class MakeGhostReadyForAutomaticJumpSystem : ForEachCommandUpdateSystem<MakeGhostReadyForAutomaticJumpCommand>
    {
        private readonly GameContext _game;

        public MakeGhostReadyForAutomaticJumpSystem(
            ICommandBuffer commandBuffer, 
            GameContext game
        ) : base(commandBuffer)
        {
            _game = game;
        }

        protected override void Execute(ref MakeGhostReadyForAutomaticJumpCommand command)
        {
            var ghost = _game.GetEntityWithUid(command.GhostUid);
            
            ghost.ReplaceGhostNeedJumpType(command.JumpType);
            ghost.ReplaceVerticalSpeed(0f);
        }
    }
}
#endif