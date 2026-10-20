using Game.Utils.Obstacles;
using R3;

namespace Game.Services.ComboWords
{
    public interface IComboWordsService
    {
        ReadOnlyReactiveProperty<string> ComboWord { get; }
        void ApplyComboType(EComboWordType comboWordType);
    }
}