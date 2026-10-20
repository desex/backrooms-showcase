using System;

namespace Project.Ui.Loading.Controllers
{
    public interface IReloadingController
    {
        void CloseWithAnimation(Action onComplete);
    }
}