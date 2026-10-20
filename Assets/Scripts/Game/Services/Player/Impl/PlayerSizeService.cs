using System;
using Db.Player;
using Ecs.Utils.Player;

namespace Game.Services.Player.Impl
{
    public class PlayerSizeService : IPlayerSizeService
    {
        private readonly GameContext _game;
        private readonly IPlayerSlideAndJumpParameters _playerSlideParameters;
        
        public PlayerSizeService(
            GameContext game,
            IPlayerSlideAndJumpParameters playerSlideParameters
        )
        {
            _game = game;
            _playerSlideParameters = playerSlideParameters;
        }
        
        public void SetCharacterControllerSize(EPlayerSizeType sizeType)
        {
            var player = _game.PlayerEntity;
            
            if (!player.HasCharacterController)
                return;
            
            var characterController = player.CharacterController.Value;
            var defaultRadius = _playerSlideParameters.DefaultCharacterControllerRadiusSize;
            var controllableSlideRadius = _playerSlideParameters.ControllableSlideCharacterControllerRadiusSize;

            switch (sizeType)
            {
                case EPlayerSizeType.Default:
                    characterController.radius = defaultRadius;
                    break;
                case EPlayerSizeType.ControllableSlide:
                    characterController.radius = controllableSlideRadius;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sizeType), sizeType, null);
            }
            
            player.ReplaceCharacterController(characterController);
        }
    }
}