using Cysharp.Threading.Tasks;

namespace Plugins.SimpleAdWrapper.Wrappers
{
    public interface IAdWrapper : IAdEvents
    {
        string AdDealerName { get; }

        bool IsAdsForbidden { get; }
        
        UniTask<bool> TryPreloadAsync();

        bool IsLoaded();

        UniTask<bool> WaitLoaded();
        
        UniTask<ShowAdResult> ShowAsync(string place);
    }
}