using BehaviorDesigner.Runtime.Tasks;
using Db.Player;
using Generated.Commands;
using JCMG.EntitasRedux.Commands;
using Zenject;

namespace Game.Models.Ai.Nodes.Impl.Tasks
{
    public class StartJump : AEntityAction
    {
        private IPlayerSlideAndJumpParameters _playerSlideAndJumpParameters;
        private ICommandBuffer _commandBuffer;

        [Inject] 
        private void Construct(
            ICommandBuffer commandBuffer, 
            IPlayerSlideAndJumpParameters playerSlideAndJumpParameters
        )
        {
            _commandBuffer = commandBuffer;
            _playerSlideAndJumpParameters = playerSlideAndJumpParameters;
        }
        
        public override TaskStatus OnUpdate()
        {
            var jumpHeight = _playerSlideAndJumpParameters.JumpHeight;
            _commandBuffer.Jump(SelfUid, jumpHeight);
            return base.OnUpdate();
        }
    }
}