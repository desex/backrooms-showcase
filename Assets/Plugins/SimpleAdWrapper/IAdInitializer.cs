using System.Threading;
using Cysharp.Threading.Tasks;

namespace Plugins.SimpleAdWrapper
{
    public interface IAdInitializer
    {
        bool SdkInitialized { get; }
        string AdDealer { get; }

        UniTask InitializeAsync(CancellationToken cancellationToken = default);
    }
}