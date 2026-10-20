#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
namespace Game.Services.DebugService
{
    public interface IDebugService
    {
        bool IsAdEnable { get; set; }
        void Freeze();
    }
}
#endif