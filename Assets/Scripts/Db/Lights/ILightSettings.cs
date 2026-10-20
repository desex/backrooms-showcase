using Game.Utils.Lights;
using UnityEngine;

namespace Db.Lights
{
    public interface ILightSettings
    {
        Material LampMaterial { get; }
        int PlayingRandomChance { get; }

        LightActionsContainer GetLightEvents(ELightEvent lightEvent);
    }
}