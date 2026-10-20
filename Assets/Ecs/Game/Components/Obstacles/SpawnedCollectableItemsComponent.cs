using Ecs.Managers;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Obstacles
{
    [Game]
    public class SpawnedCollectableItemsComponent : IComponent
    {
        public Uid[] Value;
    }
}