using Ecs.Utils.ItemCollectables;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Collectables
{
    [Game]
    public class CollectableItemComponent : IComponent
    {
        public ECollectableType Value;
    }
}