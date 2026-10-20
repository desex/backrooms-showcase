using Game.Services.Chaser;
using R3;

namespace Tests.Helpers.Mock
{
    public static partial class Mock
    {
        public class ChaserPresentationService : IChaserPresentationService
        {
            public Observable<Unit> StartAnimationCommand { get; } = new ReactiveCommand();
            public Observable<Unit> StopAnimationCommand { get; } = new ReactiveCommand();
            
            public void StopPresentation()
            {
                
            }
        }
    }
}