#if !DISABLE_GHOSTS
using Ecs.Utils;
using Game.Services.Player;
using Generated.Commands;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems.Health
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 666, nameof(EFeatures.Common))]
    public class ReviveGhostSystem : AReviveSystem
    {
        private readonly ICommandBuffer _commandBuffer;
        private readonly GameContext _game;

        public ReviveGhostSystem(
            ICommandBuffer commandBuffer, 
            GameContext game, 
            IPlayerMovementTypeService movementTypeService,
            IPlayerSizeService playerSizeService
        ) : base(commandBuffer, game, movementTypeService, playerSizeService)
        {
            _commandBuffer = commandBuffer;
            _game = game;
        }
        
        protected override bool IsNeedCharacter(GameEntity character) => character.IsGhost;

        protected override void HandleRevive(GameEntity character)
        {
            character.IsActive = false;
            character.IsInAction = false;
            character.IsGhostFarAhead = false;

            if (character.HasReadyForControllableSlide)
                character.RemoveReadyForControllableSlide();

            if (character.HasControllableSlideDirection)
                character.RemoveControllableSlideDirection();
            
            if (character.HasGhostNeedJumpType)
                character.RemoveGhostNeedJumpType();

            if (character.HasGhostAvoidanceZones)
                character.RemoveGhostAvoidanceZones();
            
            if (character.HasGhostRouteData)
                character.RemoveGhostRouteData();
            
            if (character.HasGhostCurrentRouteStatus)
                character.RemoveGhostCurrentRouteStatus();
            
            if (character.HasReviveObstacle)
                character.RemoveReviveObstacle();

            if (character.HasRevive)
                character.RemoveRevive();
            
            var player = _game.PlayerEntity;

            if (player.HasVelocity)
            {
                var playerVelocity = player.Velocity.Value;
                character.ReplaceVelocity(playerVelocity);
            }

            if (player.HasForwardSpeed)
            {
                var playerForwardSpeed = player.ForwardSpeed.Value;
                character.ReplaceForwardSpeed(playerForwardSpeed);
            }
            
            _commandBuffer.RandomNewGhostChances(character.Uid.Value);
            
            _commandBuffer.StartGhostRevive(character.Uid.Value);
        }
    }
}
#endif