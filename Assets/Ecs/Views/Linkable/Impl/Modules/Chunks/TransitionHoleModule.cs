using Core.Utils;
using Core.Utils.TriggerCollision;
using Ecs.Utils;
using Ecs.Views.Linkable.Impl.Modules.Obstacles.Jobs;
using Game.Utils;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.Chunks
{
    public class TransitionHoleModule : AObjectViewModule
    {
        [SerializeField]
        private Box _trigger;

        private GameContext _game;
        private IPlayerBoxTracker _playerBoxTracker;
        private int _triggerUid;

        [Inject]
        public void Construct(GameContext game, IPlayerBoxTracker playerBoxTracker)
        {
            // Более показать не могу
        }

        public override void Activate()
        {
            // Более показать не могу
        }

        public override void Deactivate()
        {
            // Более показать не могу
        }

        public override void DrawGizmos()
        {
            // Более показать не могу
        }
    }
}