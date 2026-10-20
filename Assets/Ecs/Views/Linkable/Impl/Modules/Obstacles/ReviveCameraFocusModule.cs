using Core.Utils.TriggerCollision;
using Ecs.Utils;
using Ecs.Views.Linkable.Impl.Modules.Obstacles.Jobs;
using Game.Models.Camera;
using Game.Services.Camera;
using Game.Services.TimeScale;
using Game.Utils;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;
using JCMG.EntitasRedux.Core.Utils;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.Obstacles
{
    public partial class ReviveCameraFocusModule : AObjectViewModule
    {
        [SerializeField]
        private Box _trigger;

        [SerializeField]
        private Transform _lookAtTarget;

        private ICameraService _cameraService;
        private GameContext _game;

        private bool _isTriggerActive;
        private IPlayerBoxTracker _playerBoxTracker;
        private GameEntity _self;

        private ISlowingDownReviveService _slowingDownReviveService;
        private int _triggerUid;

        [Inject]
        private void Construct(
            ICommandBuffer commandBuffer,
            ISlowingDownReviveService slowingDownReviveService,
            IPlayerBoxTracker playerBoxTracker,
            ICameraService cameraService,
            GameContext game
        )
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

        private void OnReviveTriggerEnter()
        {
            // Более показать не могу
        }

        private void OnReviveTriggerExit()
        {
            // Более показать не могу
        }
    }
}