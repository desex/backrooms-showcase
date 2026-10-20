using BehaviorDesigner.Runtime.Tasks;
using Generated.Commands;
using JCMG.EntitasRedux.Commands;
using Zenject;

namespace Game.Models.Ai.Nodes.Impl.Tasks
{
    public class StartDoubleJump : AEntityAction
    {
        private ICommandBuffer _commandBuffer;

        [Inject] 
        private void Construct(ICommandBuffer commandBuffer)
        {
            _commandBuffer = commandBuffer;
        }
        
        public override TaskStatus OnUpdate()
        {
            _commandBuffer.DoubleJump(SelfUid);
            return base.OnUpdate();
        }
    }
}