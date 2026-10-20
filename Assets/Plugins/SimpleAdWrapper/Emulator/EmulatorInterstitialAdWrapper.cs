using System;
using Cysharp.Threading.Tasks;
using Plugins.SimpleAdWrapper.Emulator.Settings;
using Plugins.SimpleAdWrapper.Wrappers;
using R3;
using UnityEngine;

namespace Plugins.SimpleAdWrapper.Emulator
{
    public class EmulatorInterstitialAdWrapper : IInterstitialAdWrapper, IDisposable
    {
        private readonly ReactiveCommand<AdStateUpdate> _onStateCommand = new();

        private readonly IAdEmulatorSettingsBase _settings;
        private readonly IAdStatesPatternBase _patterns;

        private InternalState _state;

        public Observable<AdStateUpdate> OnState => _onStateCommand;
        public string AdDealerName => "Emulator";
        public bool IsAdsForbidden => false;

        public EmulatorInterstitialAdWrapper(IAdEmulatorSettingsBase settings, IAdStatesPatternBase patterns)
        {
            _settings = settings;
            _patterns = patterns;
        }

        public async UniTask<bool> TryPreloadAsync()
        {
            if (_state.IsLoaded)
                return true;
            
            UpdateState(EAdState.Load);

            var pattern = _settings.PreloadPattern;

            await UniTask.Delay(TimeSpan.FromSeconds(pattern.loadDelay));

            if (!pattern.loadSuccess)
            {
                _state.IsLoaded = false;
                UpdateStateError(EAdState.FailToLoad, "Ad load failed");
                return false;
            }

            _state.IsLoaded = true;
            UpdateState(EAdState.Loaded);

            return true;
        }

        public bool IsLoaded()
        {
            return _state.IsLoaded;
        }

        public UniTask<bool> WaitLoaded() => UniTask.FromResult(_state.IsLoaded);

        public async UniTask<ShowAdResult> ShowAsync(string place)
        {
            if (_state.ShowCompletionSource != default)
                throw new("Already in progress");

            _state.Place = place;
            _state.StartTime = DateTimeOffset.Now;

            var placeSettings = _settings.GetPlace(place, EAdType.Video);

            if (string.IsNullOrWhiteSpace(placeSettings.pattern))
                throw new($"Place: {place} without pattern");

            Debug.Log($"[{nameof(EmulatorRewardVideoAdWrapper)}] Place: {place} - use pattern: {placeSettings.pattern}");

            var pattern = _patterns.GetPattern(placeSettings.pattern);

            foreach (var state in pattern.states)
            {
                if (state.delayBefore != 0)
                    await UniTask.Delay(TimeSpan.FromSeconds(state.delayBefore));

                if (state is { adState: EAdState.Close, finishedWithReward: false })
                    _state.WasSkipped = true;

                if (state.adState == EAdState.Show)
                {
                    _state.WasShown = true;
                    _state.IsLoaded = false;
                }

                if (state.adState == EAdState.Clicked)
                    _state.WasClicked = true;

                if (state.adState is EAdState.Fail or EAdState.FailToLoad or EAdState.FailToPlay)
                {
                    _state.WasFail = true;
                    UpdateStateError(state.adState, state.failReason);
                }
                else
                    UpdateState(state.adState);

                if (state.delayAfter != 0)
                    await UniTask.Delay(TimeSpan.FromSeconds(state.delayAfter));
            }

            var result = GetResult();

            Reset();

            return result;
        }

        private void Reset()
        {
            _state = new();
        }

        private ShowAdResult GetResult() => new()
        {
            AdType = EAdType.Video,
            AdDealer = "Emulator",
            WasClicked = _state.WasClicked,
            WasShown = _state.WasShown,
            WasFailed = _state.WasFail,
            WasSkipped = _state.WasSkipped
        };

        private void UpdateState(EAdState state)
        {
            _onStateCommand.Execute(new()
                {
                    AdType = EAdType.Video,
                    AdDealer = AdDealerName,
                    State = state,
                    Place = _state.Place,
                    Duration = (DateTimeOffset.Now - _state.StartTime).TotalSeconds,
                    Error = null
                }
            );
        }

        public void UpdateStateError(EAdState state, string error)
        {
            _onStateCommand.Execute(new()
                {
                    AdType = EAdType.Video,
                    AdDealer = AdDealerName,
                    State = state,
                    Place = _state.Place,
                    Duration = (DateTimeOffset.Now - _state.StartTime).TotalSeconds,
                    Error = error
                }
            );
        }

        public void Dispose()
        {
            _onStateCommand?.Dispose();
            _state.ShowCompletionSource?.TrySetCanceled();
        }

        private struct InternalState
        {
            public UniTaskCompletionSource ShowCompletionSource;

            public bool IsLoaded;

            public bool WasClicked;
            public bool WasShown;
            public bool WasFail;
            public bool WasSkipped;
            public string Place;
            public DateTimeOffset StartTime;
        }
    }
}