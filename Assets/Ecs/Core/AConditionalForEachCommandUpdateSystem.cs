using JCMG.EntitasRedux.Commands;

namespace Ecs.Core
{
    public abstract class AConditionalForEachCommandUpdateSystem<TCommand> : ForEachCommandUpdateSystem<TCommand> where TCommand : struct
    {
        protected AConditionalForEachCommandUpdateSystem(ICommandBuffer commandBuffer) : base(commandBuffer)
        {
            
        }
        
        protected abstract bool ConditionFilter();

        protected override void Execute(ref TCommand command)
        {
            if (ConditionFilter())
                ExecuteCommand(ref command);
        }

        protected abstract void ExecuteCommand(ref TCommand command);
    }
}