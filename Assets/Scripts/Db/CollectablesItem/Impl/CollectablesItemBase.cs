using System.Collections.Generic;
using Db.CollectablesItem.Vo;
using Ecs.Utils.ItemCollectables;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Db.CollectablesItem.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(ICollectablesItemBase), fileName = nameof(ICollectablesItemBase))]
    public class CollectablesItemBase : SerializedScriptableObject, ICollectablesItemBase
    {
        [SerializeField] private Dictionary<ECollectableType, ItemParameterVo> _collectableVos;

        public IReadOnlyDictionary<ECollectableType, ItemParameterVo> CollectableVos => _collectableVos;
    }
}