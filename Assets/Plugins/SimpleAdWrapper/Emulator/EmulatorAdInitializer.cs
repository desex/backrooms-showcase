using System.Threading;
using Cysharp.Threading.Tasks;

namespace Plugins.SimpleAdWrapper.Emulator
{
    public class EmulatorAdInitializer : IAdInitializer
    {
        public bool SdkInitialized { get; private set; }
        public string AdDealer => "Emulator";
        
        public UniTask InitializeAsync(CancellationToken cancellationToken = default)
        {
            SdkInitialized = true;

            return UniTask.CompletedTask;
        }
    }
}