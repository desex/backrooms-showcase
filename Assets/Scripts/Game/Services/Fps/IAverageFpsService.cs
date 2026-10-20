using Cysharp.Threading.Tasks;

namespace Game.Services.Fps
{
    public interface IAverageFpsService
    {
        UniTask Start();
        public int GetAverageFps();
    }
}