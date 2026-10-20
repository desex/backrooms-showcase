using Game.Services.General;
using JCMG.EntitasRedux.Commands;
using R3;
using R3.Triggers;
using UnityEngine;
using Zenject;

namespace Tools
{
    public class PlayerTeleporter : MonoBehaviour
    {
        private const string PLAYER_NAME = "Player";

        [Inject]
        private GameContext _game;

        [Inject]
        private ICommandBuffer _commandBuffer;

        public BoxCollider StartCollider;
        public BoxCollider FinishCollider;

        private bool _isInitialised;
        private GameEntity _player;
        private Vector3 _startPosition;
        private Quaternion _startRotation;

        private void Update()
        {
            // Более показать не могу
        }

        private void Init()
        {
            // Более показать не могу
        }

        private void OnStartTriggerEnter(Collider other)
        {
            // Более показать не могу
        }

        private void OnFinishTriggerEnter(Collider other)
        {
            // Более показать не могу
        }
    }
}