using System;
using Ecs.Utils;
using Game.Models.Camera.Impl;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using R3;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Camera
{
    public class MainMenuCameraView : ObjectView
    {
        [Inject] private readonly PlayerCameraHolder _playerCameraHolder;
        [Inject] private readonly GameContext _game;
        
        [SerializeField] private UnityEngine.Camera _mainMenuCamera;

        private IDisposable _updateCameraPositionDisposable;

        protected override void Subscribe(IEntity entity, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }

        private void OnGameState(GameEntity entity, EGameState value)
        {
            // Более показать не могу
        }

        private void StartUpdatingCameraPosition()
        {
            // Более показать не могу
        }

        private void StopUpdatingCameraPosition()
        {
            // Более показать не могу
        }

        private void UpdateCameraPosition()
        {
            // Более показать не могу
        }
    }
}