using System;
using System.IO;
using System.Threading;
using Game.Utils.Lights;

namespace Game.Services.Lights.Impl.Builders.Data
{
    public class LightEventBuilderData
    {
        private CancellationTokenSource _defaultToken;
        private bool _isPlaying;
        private ELightEvent _lightEvent;
        private Action _onEnd;
        private Action _onStart;

        public LightEventBuilderData(CancellationTokenSource defaultToken)
        {
            _defaultToken = defaultToken;
        }

        public void Reset()
        {
            // Более показать не могу
        }

        public void SetEvent(ELightEvent lightEvent)
        {
            // Более показать не могу
        }

        public CancellationTokenSource GetToken()
        {
            // Более показать не могу
            return default;
        }

        public void SetOnEnd(Action onEnd)
        {
            // Более показать не могу
        }

        public Action GetOnEnd()
        {
            // Более показать не могу
            return default;
        }

        public ELightEvent GetLightEvent()
        {
            // Более показать не могу
            return default;
        }

        public void SetDefaultToken(CancellationTokenSource token)
        {
            // Более показать не могу
        }

        public void SetOnStart(Action onStart)
        {
            // Более показать не могу
        }

        public Action GetOnStart()
        {
            // Более показать не могу
            return default;
        }

        public void SetPlaying(bool value)
        {
            // Более показать не могу
        }

        public bool CanPlay()
        {
            // Более показать не могу
            return default;
        }
    }
}