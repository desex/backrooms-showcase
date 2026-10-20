using System;
using System.Collections.Generic;
using Game.UI.ChaseProgressBar;
using Game.UI.GameRestartButton;
using Game.UI.UIBase;
using R3;
using SimpleUi.Abstracts;
using SimpleUi.Interfaces;
using UnityEngine;

namespace Game.Services.UiVisibility.Impl
{
    public class UiVisibilityService : IUiVisibilityService, IDisposable
    {
#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
        private readonly IDisposable _disposable;
#endif
        private readonly List<UiView> _views = new List<UiView>();

        private bool _isDebugUiActive = true;

        public UiVisibilityService(IEnumerable<IUiView> views)
        {
            foreach (var view in views)
            {
                if (view is IUiDisablable)
                    _views.Add(view as UiView);
            }

#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
            _disposable = Observable.EveryUpdate().Subscribe(_ => Update());
#endif
        }

        public void Dispose()
        {
#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
            _disposable?.Dispose();
#endif
        }

        public void ChangeChaserUiVisibility(bool state)
        {
            // Более показать не могу
        }

        public void ChangeOtherUiVisibility(bool state)
        {
            foreach (var view in _views)
            {
                var viewType = view.GetType();
                
                if (viewType == typeof(ChaserView))
                    continue;

#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
                if (viewType == typeof(DebugView))
                    continue;
#endif

                view.gameObject.SetActive(state && view.IsShow);
            }
        }

        private void FindViewAndChangeState<T>(bool targetState)
        {
            // Более показать не могу
        }

#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
        private void Update()
        {
            // Более показать не могу
        }

        private void ToggleDebugView()
        {
            // Более показать не могу
        }
#endif
    }
}