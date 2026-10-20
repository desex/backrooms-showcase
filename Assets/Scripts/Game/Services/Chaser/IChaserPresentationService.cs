using R3;

namespace Game.Services.Chaser
{
    public interface IChaserPresentationService
    {
        Observable<Unit> StartAnimationCommand { get; }
        Observable<Unit> StopAnimationCommand { get; }
    }
}