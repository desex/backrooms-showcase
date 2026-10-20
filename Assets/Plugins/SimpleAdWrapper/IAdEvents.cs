using R3;

namespace Plugins.SimpleAdWrapper
{
    public interface IAdEvents
    {
        /// <summary>
        /// any ad state changes
        /// </summary>
        Observable<AdStateUpdate> OnState { get; }
    }
}