using Game.UI.Tutorial;
using SimpleUi;
using SimpleUi.Interfaces;

namespace Game.UI
{
    public class TutorialFailWindow : WindowBase, IPopUp
    {
        public override string Name => nameof(TutorialFailWindow);
        
        protected override void AddControllers()
        {
            // Более показать не могу
        }
    }
}