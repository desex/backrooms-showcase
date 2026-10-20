using Ecs.Utils.ItemCollectables;
using Ecs.Views.Linkable.Impl.Collectables;

namespace Game.Services.CollectablesItem
{
    public interface ICollectablesPool
    {
        public ItemCollectableView SpawnCollectable(ECollectableType collectableType);

        public void DespawnCollectable(ECollectableType collectableType, ItemCollectableView collectable);
    }
}