using Game.Utils.Rebound;
using R3;

namespace Game.Utils.Collisions
{
    public interface ICollisionService
    {
        ReadOnlyReactiveProperty<bool> IsPlayerCollisionEnabled { get; }
        void EnablePlayerCollisions();
        void DisablePlayerCollisions();
        void Register(int hashCode, ReboundSetup reboundSetup);
        bool TryGet(int hashCode, out ReboundSetup reboundSetup);
    }
}