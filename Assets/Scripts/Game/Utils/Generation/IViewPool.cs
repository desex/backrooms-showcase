using Ecs.Managers;
using R3;

namespace Game.Utils.Generation
{
    public interface IViewPool<TObject>
    {
        public Observable<Uid> OnViewOfEntityDestroyed { get; }
    }
}