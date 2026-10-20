using System;
using Game.Utils.Movement;

namespace Game.Services.Obstacles
{
    public interface ITravelatorService
    {
        void ActivateTravelator(ETravelatorDirection direction, ETravelatorSize size);
        void DeactivateTravelator(Action onTravelatorExit, ETravelatorDirection direction, ETravelatorSize size);
        void KillTravelatorWork();
    }
}