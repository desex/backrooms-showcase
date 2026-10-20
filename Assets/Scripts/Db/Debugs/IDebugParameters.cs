#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
namespace Db.Debugs
{
    public interface IDebugParameters
    {
        float FreezeDuration { get; }
    }
}
#endif