using Ecs.Core.Interfaces;
using JCMG.EntitasRedux;

namespace Ecs.Core
{
    public abstract class AFixedTimeUpdateSystem : IUpdateSystem
    {
        private readonly ITimeProvider _timeProvider;
        private float _lastTime;
        
        protected AFixedTimeUpdateSystem(ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }
        
        protected abstract float FixedUpdateTime { get; }

        public void Update()
        {
            var currentTime = _timeProvider.Time;

            if (currentTime - _lastTime < FixedUpdateTime)
                return;
            
            _lastTime = currentTime;
            Execute();
        }

        protected abstract void Execute();
    }
}