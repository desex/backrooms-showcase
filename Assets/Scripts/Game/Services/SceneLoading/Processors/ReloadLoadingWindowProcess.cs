using System;
using PdUtils.SceneLoadingProcessor.Impls;
using Project.Ui.Loading;
using R3;
using SimpleUi.Signals;
using Zenject;

namespace Game.Services.SceneLoading.Processors
{
    public class ReloadLoadingWindowProcess : Process
    {
        private readonly SignalBus _signalBus;

        public ReloadLoadingWindowProcess(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public override void Do(Action onComplete)
        {
            _signalBus.OpenWindow<ReloadingWindow>(EWindowLayer.Project);

            Observable.NextFrame()
                .Subscribe(_ => onComplete());
        }
    }
}