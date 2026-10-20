using BehaviorDesigner.Runtime;
using Game.Providers;
using Zenject;

namespace Game.Services.Ai.Impl
{
    public class BehaviourTreesService : IBehaviourTreesService, IInitializable
    {
        private readonly IGameFieldProvider _gameFieldProvider;
        private BehaviorManager _behaviorManager;

        public BehaviourTreesService(IGameFieldProvider gameFieldProvider)
        {
            _gameFieldProvider = gameFieldProvider;
        }

        public void Update()
        {
            // Более показать не могу
        }

        public void EnableAndResetBehavior(BehaviorTree tree)
        {
            // Более показать не могу
        }

        public void SetBehaviorPause(BehaviorTree tree, bool isPaused)
        {
            // Более показать не могу
        }

        public void DisableBehavior(BehaviorTree tree)
        {
            // Более показать не могу
        }

        public void Initialize()
        {
            // Более показать не могу
        }
    }
}