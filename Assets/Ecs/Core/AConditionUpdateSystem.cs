using JCMG.EntitasRedux;

namespace Ecs.Core
{
    public abstract class AConditionUpdateSystem : IUpdateSystem
    {
        protected abstract bool ConditionFilter();
        protected abstract void UpdateSystem();
        
        private bool IsUpdate()
        {
            return ConditionFilter();
        }

        public void Update()
        {
            if (IsUpdate())
                UpdateSystem();
        }
    }
}