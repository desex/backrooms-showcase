using Core.Utils.TriggerCollision;
using Ecs.Managers;
using Ecs.Utils;
using Ecs.Views.Linkable.Impl.Modules.Obstacles.Jobs;
using Game.Utils;
using Generated.Commands;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;
using JCMG.EntitasRedux.Core.Utils;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.Movement.Slide
{
    public partial class SlideTriggerModule : AObjectViewModule
    {
        [SerializeField]
        private Box _trigger;

        private ICommandBuffer _commandBuffer;
        private GameEntity _entitySelf;
        private GameContext _game;
        private IPlayerBoxTracker _playerBoxTracker;
        private int _triggerUid;

        [Inject]
        private void Construct(ICommandBuffer commandBuffer, GameContext game, IPlayerBoxTracker playerBoxTracker)
        {
            // Более показать не могу
        }

        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
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

        private void SetStartControllableSlide(Uid targetUid)
        {
            // Более показать не могу
        }
    }
}