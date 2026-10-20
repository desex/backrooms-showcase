using System;
using Ecs.Utils.ItemCollectables;

namespace Game.DaoData.Collectibles
{
    [Serializable]
    public struct CollectiblesData
    {
        public ECollectableType Type;
        public int Count;
    }
}