using Ecs.Utils.Player;

namespace Game.Services.Player
{
    public interface IPlayerSizeService
    {
        void SetCharacterControllerSize(EPlayerSizeType sizeType);
    }
}