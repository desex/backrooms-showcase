using Ecs.Core.Systems;
using Ecs.Utils;
using Ecs.Utils.Groups;
using Game.Utils.Movement;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;

namespace Ecs.Action.Systems.Movement
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 805, nameof(EFeatures.Move))]
    public sealed class FastActionMoveDisableSystem : ACharacterUpdateSystem
    {
        public FastActionMoveDisableSystem(IGameGroupUtils gameGroupUtils) : base(gameGroupUtils)
        {
        }

        protected override bool EntityFilter(GameEntity entity)
        {
            return !entity.IsReadyForFastActionMove
                   && entity.IsFastActionMove
                   && entity.HasBodyMoveType
                   && IsFastActionCanBeDisabledInMoveType(entity)
                   && !entity.IsDestroyed;
        }

        protected override void HandleUpdate(GameEntity entity)
        {
            entity.IsFastActionMove = false;
        }

        private static bool IsFastActionCanBeDisabledInMoveType(GameEntity player)
        {
            var isJump = player.HasJumping
                         && player.Jumping.Value is EJumpType.MonkeyJump
                             or EJumpType.Jump
                             or EJumpType.JumpOn
                             or EJumpType.ForwardJump
                             or EJumpType.JumpInObstacle;

            var isSlide = player.HasSliding;
            var isClimb = player.IsClimbing;

            return !isJump && !isSlide && !isClimb;
        }
    }
}