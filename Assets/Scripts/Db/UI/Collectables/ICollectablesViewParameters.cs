using Db.CollectablesItem.Vo;

namespace Db.UI.Collectables
{
    public interface ICollectablesViewParameters
    {
        CollectableUiAnimationVo CollectiblesParameters { get; }
        CollectableUiAnimationVo ComboCollectiblesParameters { get; }
    }
}