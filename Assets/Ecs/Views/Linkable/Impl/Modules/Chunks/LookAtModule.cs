using System.Collections.Generic;
using Core.Utils.TriggerCollision;
using Ecs.Utils;
using Ecs.Views.Linkable.Impl.Modules.Obstacles.Jobs;
using Game.Models.Camera;
using Game.Services.Camera;
using Game.Utils;
using Game.Utils.SerializeCollection.Impl.Dictionary;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.Chunks
{
    public partial class LookAtModule : AObjectViewModule
    {
        [SerializeField]
        private DictionaryTransitionHoleZones _lookAtAnchors;

        [SerializeField]
        private Box _triggerInZoneHole;

        [SerializeField]
        private Box[] _triggersOutZoneHole;

        private readonly Stack<int> _triggersUids = new Stack<int>();

        private ICameraService _cameraService;
        private GameContext _game;
        private IPlayerBoxTracker _playerBoxTracker;
        
        [Inject]
        public void Construct(ICameraService cameraService, IPlayerBoxTracker playerBoxTracker, GameContext game)
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

        private void OnLookAtEnable(Box enteredBox)
        {
            // Более показать не могу
        }

        private void DisableLookAt()
        {
            // Более показать не могу
        }

        private Transform FindNearestLookAtAnchor(Box other)
        {
            // Более показать не могу
            return default;
        }
    }
}