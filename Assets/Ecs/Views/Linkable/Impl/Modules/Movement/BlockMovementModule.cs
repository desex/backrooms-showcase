using Core.Utils.TriggerCollision;
using Ecs.Utils;
using Ecs.Views.Linkable.Impl.Modules.Obstacles.Jobs;
using Game.Utils;
using Game.Utils.MovementBlock;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.Movement
{
    public partial class BlockMovementModule : AObjectViewModule
    {
        [SerializeField]
        private Box _trigger;

        [SerializeField]
        private EBlockedMovement _movementsToBlock;

        private GameContext _game;
        private IPlayerBoxTracker _playerBoxTracker;
        private int _triggerUid;

        [Inject]
        private void Construct(GameContext game, IPlayerBoxTracker playerBoxTracker)
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

        private void ChangeMovementsState(bool blocked)
        {
            // Более показать не могу
        }
    }
}