using Generated.Commands;
using JCMG.EntitasRedux.Commands;
using PdUtils.Interfaces;
using SimpleUi.Abstracts;
using SimpleUi.Signals;
using Zenject;

namespace Game.UI.Tutorial
{
    public class TutorialFailController : UiController<TutorialFailView>, IUiInitializable
    {
        private readonly ICommandBuffer _commandBuffer;
        private readonly SignalBus _signalBus;

        public TutorialFailController(
            ICommandBuffer commandBuffer,
            SignalBus signalBus
        )
        {
            _commandBuffer = commandBuffer;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            // Более показать не могу
        }

        public override void OnShow()
        {
            // Более показать не могу
        }

        private void OnFadeOutStart()
        {
            // Более показать не могу
        }
        
        private void OnFadeOutComplete()
        {
            // Более показать не могу
        }
    }
}