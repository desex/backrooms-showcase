using BehaviorDesigner.Runtime;

namespace Game.Services.Ai
{
    public interface IBehaviourTreesService
    {
        void Update();
        void EnableAndResetBehavior(BehaviorTree tree);
        void SetBehaviorPause(BehaviorTree tree, bool isPaused);
        void DisableBehavior(BehaviorTree tree);
    }
}