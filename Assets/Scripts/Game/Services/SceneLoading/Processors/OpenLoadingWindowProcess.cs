using System;
using PdUtils.SceneLoadingProcessor.Impls;
using Project.Ui.Loading;
using R3;
using SimpleUi.Signals;
using Zenject;

namespace Game.Services.SceneLoading.Processors
{
    public class OpenLoadingWindowProcess : Process
    {
        private readonly SignalBus _signalBus;

        public OpenLoadingWindowProcess(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public override void Do(Action complete)
        {
            _signalBus.OpenWindow<LoadingWindow>(EWindowLayer.Project);

            Observable.NextFrame()
                .Subscribe(_ => complete());
        }
    }
}