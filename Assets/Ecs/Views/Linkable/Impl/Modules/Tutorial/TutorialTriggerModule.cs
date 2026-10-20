using Core.Utils.TriggerCollision;
using Ecs.Managers;
using Ecs.Utils;
using Ecs.Views.Linkable.Impl.Modules.Obstacles.Jobs;
using Game.Utils;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.Tutorial
{
    public partial class TutorialTriggerModule : AObjectViewModule
    {
        [SerializeField]
        private Box _trigger;

        [SerializeField]
        private Color _color;

        private GameContext _game;
        private IPlayerBoxTracker _playerBoxTracker;

        private Uid _selfUid;
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

        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }

        private void OnPlayerTriggerExit()
        {
            // Более показать не могу
        }
    }
}