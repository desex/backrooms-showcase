using System.Collections.Generic;
using Db.CollectablesItem.Vo;
using Ecs.Utils.ItemCollectables;

namespace Db.CollectablesItem
{
    public interface ICollectablesItemBase
    {
        IReadOnlyDictionary<ECollectableType, ItemParameterVo> CollectableVos { get; }
    }
}