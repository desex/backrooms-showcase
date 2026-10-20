using Db.CollectablesItem.Vo;
using UnityEngine;

namespace Db.UI.Collectables.Impl
{
    [CreateAssetMenu(menuName = "Settings/UI/" + nameof(CollectablesViewParameters), fileName = nameof(CollectablesViewParameters))]
    public class CollectablesViewParameters : ScriptableObject, ICollectablesViewParameters
    {
        [field: SerializeField] public CollectableUiAnimationVo CollectiblesParameters { get; private set; }
        [field: SerializeField] public CollectableUiAnimationVo ComboCollectiblesParameters { get; private set; }
    }
}