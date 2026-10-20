using Core.Utils.TriggerCollision;
using Ecs.Utils;
using Ecs.Views.Linkable.Impl.Modules.Obstacles.Jobs;
using Game.Utils;
using Game.Utils.Sound;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules
{
    public partial class SurfaceInteractionModule : AObjectViewModule
    {
        [SerializeField]
        private Box _surfaceBoxTrigger;

        [SerializeField]
        private ESurfaceInteractionType _surfaceInteractionType;

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

        private void OnSoundTriggerEnter()
        {
            // Более показать не могу
        }

        private void OnSoundTriggerExit()
        {
            // Более показать не могу
        }
    }
}