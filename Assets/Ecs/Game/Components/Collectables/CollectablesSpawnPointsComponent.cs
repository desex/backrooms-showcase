using JCMG.EntitasRedux;
using UnityEngine;

namespace Ecs.Game.Components.Collectables
{
    [Game]
    public class CollectablesSpawnPointsComponent : IComponent
    {
        public Vector3[] Value;
    }
}