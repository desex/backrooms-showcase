using Ecs.Managers;
using Game.Utils.Movement;

namespace Game.Services.Player
{
    public interface IPlayerMovementTypeService
    {
        void UpdateBodyType(Uid characterUid);
        void ChangeBodyMoveType(Uid playerUid, EBodyMoveType movementType);
    }
}