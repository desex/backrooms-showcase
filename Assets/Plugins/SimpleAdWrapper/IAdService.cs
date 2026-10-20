using Cysharp.Threading.Tasks;

namespace Plugins.SimpleAdWrapper
{
    public interface IAdService : IAdEvents
    {
        UniTask<bool> TryPreloadAsync();
        
        bool IsLoaded();
        
        UniTask<bool> WaitLoaded();
        
        UniTask<ShowAdResult> ShowAdAsync(string place);
    }
}