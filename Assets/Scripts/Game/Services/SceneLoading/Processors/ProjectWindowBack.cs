using System;
using PdUtils.SceneLoadingProcessor.Impls;
using R3;
using SimpleUi.Signals;
using Zenject;

namespace Game.Services.SceneLoading.Processors
{
    public class ProjectWindowBack : Process
    {
        private readonly SignalBus _signalBus;

        public ProjectWindowBack(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public override void Do(Action onComplete)
        {
            _signalBus.BackWindow(EWindowLayer.Project);

            Observable.NextFrame()
                .Subscribe(_ => onComplete());
        }
    }
}