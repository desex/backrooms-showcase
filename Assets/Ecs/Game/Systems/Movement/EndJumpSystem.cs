using System.Collections.Generic;
using Ecs.Utils;
using Game.Services.Player;
using Game.Utils.Movement;
using Generated.Commands;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Game.Systems.Movement
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 500, nameof(EFeatures.Move))]
    public class EndJumpSystem : ReactiveSystem<GameEntity>
    {
        private readonly IPlayerMovementTypeService _playerMovementTypeService;
        private readonly ICommandBuffer _commandBuffer;

        public EndJumpSystem(
            GameContext context,
            IPlayerMovementTypeService playerMovementTypeService,
            ICommandBuffer commandBuffer
        ) : base(context)
        {
            _playerMovementTypeService = playerMovementTypeService;
            _commandBuffer = commandBuffer;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
            => context.CreateCollector(GameMatcher.Grounded.Added());

        protected override bool Filter(GameEntity entity)
            => entity.IsCharacter && entity.HasJumping && IsNeedJumpingType(entity.Jumping.Value);

        protected override void Execute(IEnumerable<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.RemoveJumping();
                
                if (entity.HasReadyForControllableSlide)
                    _commandBuffer.StartSlide(entity.Uid.Value);
                else if (entity.HasBodyMoveType && entity.BodyMoveType.Value == EBodyMoveType.Jump)
                    _playerMovementTypeService.UpdateBodyType(entity.Uid.Value);
            }
        }

        private static bool IsNeedJumpingType(EJumpType jumpType) => 
            jumpType is EJumpType.Jump or EJumpType.DoubleJump;
    }
}