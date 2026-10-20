using SimpleUi;
using SimpleUi.Interfaces;

namespace Game.UI.SettingsPopup
{
    public class SettingsPopupWindow : WindowBase, IPopUp
    {
        public override string Name => "SettingsPopup";
        
        protected override void AddControllers()
        {
            // Более показать не могу
        }
    }
}