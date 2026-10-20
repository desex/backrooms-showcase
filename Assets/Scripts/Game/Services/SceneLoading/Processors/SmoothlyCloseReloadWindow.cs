using System;
using PdUtils.SceneLoadingProcessor.Impls;
using Project.Ui.Loading.Controllers;

namespace Game.Services.SceneLoading.Processors
{
    public class SmoothlyCloseReloadWindow : Process
    {
        private readonly IReloadingController _reloadingController;

        public SmoothlyCloseReloadWindow(IReloadingController reloadingController)
        {
            _reloadingController = reloadingController;
        }

        public override void Do(Action onComplete)
        {
            _reloadingController.CloseWithAnimation(onComplete);
        }
    }
}