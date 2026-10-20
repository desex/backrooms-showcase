using System.Collections.Generic;
using Animancer;
using Game.Utils;
using Game.Utils.Camera;
using Game.Utils.Collisions;
using Generated.Commands;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;
using JCMG.EntitasRedux.Core.Utils;
using UnityEngine;
using Zenject;
using R3;

namespace Ecs.Views.Linkable.Impl.Player
{
    public partial class PlayerView : CharacterView
    {
        [SerializeField]
        private AnimancerComponent _animancer;

        [SerializeField]
        private CharacterController _characterController;

        [SerializeField]
        private CameraAnchor[] _cameraAnchors;

        [SerializeField]
        private Transform _anchorsContainer;

        [SerializeField]
        private GameObject _characterTrigger;

        private GameEntity _self;
        private ICommandBuffer _commandBuffer;
        private GameContext _game;
        private ICollisionService _collisionService;

        public IEnumerable<CameraAnchor> CameraAnchors => _cameraAnchors;
        public Transform AnchorsContainer => _anchorsContainer;

        [Inject]
        private void Construct(
            ICommandBuffer commandBuffer,
            GameContext game,
            ICollisionService collisionService
        )
        {
            // Более показать не могу
        }

        protected override void Subscribe(IEntity entity, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }

        private void OnCharacterTriggerEnabled(GameEntity entity)
        {
            // Более показать не могу
        }

        private void OnPauseAnimancer(GameEntity entity, bool value)
        {
            // Более показать не могу
        }

        protected override void OnVelocity(GameEntity entity, Vector3 value)
        {
            // Более показать не могу
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            // Более показать не могу
        }
    }
}