using System;
using System.Collections.Generic;
using Core.Utils.TriggerCollision;
using Ecs.Utils;
using Ecs.Views.Linkable.Impl.Modules.Obstacles.Jobs;
using Game.Services.Score;
using Game.Utils;
using Game.Utils.Movement;
using Game.Utils.Obstacles;
using Generated.Commands;
using JCMG.EntitasRedux.Commands;
using R3;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules
{
    public partial class IncreaseScoreAfterNoDamageMovementModule : AObjectViewModule
    {
        [SerializeField]
        private DangerZoneTrigger[] _dangerZoneTriggers;

        private readonly Stack<int> _triggersUids = new Stack<int>();
        private IDisposable _characterControllerDisposable;
        private ICommandBuffer _commandBuffer;
        private GameContext _game;

        private int _healthOnEnter;

        private bool _isPlayerInTrigger;
        private IPlayerBoxTracker _playerBoxTracker;

        private IScoreService _scoreService;

        [Inject]
        public void Construct(
            ICommandBuffer commandBuffer,
            IScoreService scoreService,
            IPlayerBoxTracker playerBoxTracker,
            GameContext gameContext
        )
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

        private void OnDangerTriggerEnter(EObstacleTypeForAcceleration obstacleTypeForAcceleration)
        {
            // Более показать не могу
        }

        private void CheckCharacterControllerDisable(
            Collider characterController,
            EObstacleTypeForAcceleration obstacleTypeForAcceleration
        )
        {
            // Более показать не могу
        }

        private void HandleCharacterExit(EObstacleTypeForAcceleration obstacleTypeForAcceleration)
        {
            // Более показать не могу
        }
    }
}