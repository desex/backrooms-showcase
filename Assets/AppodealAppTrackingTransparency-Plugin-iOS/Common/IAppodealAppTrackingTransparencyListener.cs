using System.Diagnostics.CodeAnalysis;

namespace AppodealAppTracking.Unity.Common
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    public interface IAppodealAppTrackingTransparencyListener
    {
        void AppodealAppTrackingTransparencyListenerNotDetermined();
        void AppodealAppTrackingTransparencyListenerRestricted();
        void AppodealAppTrackingTransparencyListenerDenied();
        void AppodealAppTrackingTransparencyListenerAuthorized();
    }
}