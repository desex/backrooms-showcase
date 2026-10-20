#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
using System;
using System.Diagnostics;
using Db.Debugs;

namespace Game.Services.DebugService.Impl
{
    public class DebugService : IDebugService
    {
        private readonly IDebugParameters _debugParameters;

        public DebugService(IDebugParameters debugParameters)
        {
            _debugParameters = debugParameters;
        }

        public bool IsAdEnable { get; set; } = false;

        public void Freeze()
        {
            // Более показать не могу
        }
    }
}
#endif