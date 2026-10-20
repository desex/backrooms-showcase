using Core.Utils.TriggerCollision;
using Ecs.Views.Linkable.Impl.Modules.Obstacles.Jobs;
using Game.Utils;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Obstacles
{
    public abstract partial class AObstacleGroupWithMoveZonesView : ObstacleGroupView
    {
        [Space]
        [Required]
        [TabGroup(GENERAL_GROUP)]
        [SerializeField]
        private Box _readyBoxTrigger;

        [Space]
        [Required]
        [TabGroup(GENERAL_GROUP)]
        [SerializeField]
        private Box _fastActionMoveBoxTrigger;

        private int _fastActionTriggerUid;
        private IPlayerBoxTracker _playerBoxTracker;

        private int _readyTriggerUid;

        protected GameContext Game;

        [Inject]
        private void Construct(GameContext game, IPlayerBoxTracker playerBoxTracker)
        {
            // Более показать не могу
        }

        protected override void Subscribe(IEntity entity, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }

        private void OnStretchSize(GameEntity entity, float value)
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

        protected abstract void OnReadyTriggerEntered();

        protected abstract void OnReadyTriggerExited();

        protected virtual void OnFastActionMoveTriggerEntered()
        {
            // Более показать не могу
        }

        protected virtual void OnFastActionMoveTriggerExited()
        {
            // Более показать не могу
        }

        protected virtual void LinkBoxActions()
        {
            // Более показать не могу
        }

        protected virtual void UnlinkBoxActions()
        {
            // Более показать не могу
        }

        protected virtual void StretchBoxesSize(float stretchSize)
        {
            // Более показать не могу
        }
    }
}