using System;
using Project.Ui.Loading.Views;
using SimpleUi.Abstracts;

namespace Project.Ui.Loading.Controllers.Impl
{
    public class ReloadingController : UiController<ReloadingView>, IReloadingController
    {
        public void CloseWithAnimation(Action onComplete) {
            // Более показать не могу
        }
    }
}