using Ecs.Utils.ItemCollectables;
using R3.Utils;

namespace Game.Services.CollectablesItem
{
    public interface ICollectiblesStorage
    {
        IReadOnlyReactiveDictionary<ECollectableType, int> ItemsCollection { get; }
        void AddSingleCollectibleItem(ECollectableType type);
        void AddCollectibleItem(ECollectableType type, int value);
        void RemoveCollectibleItem(ECollectableType type, int count);
        bool HasCollectibleItem(ECollectableType type, int count);
    }
}