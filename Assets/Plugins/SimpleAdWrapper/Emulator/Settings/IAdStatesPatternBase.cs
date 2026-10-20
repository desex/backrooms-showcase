using Plugins.SimpleAdWrapper.Emulator.Settings.Impl;

namespace Plugins.SimpleAdWrapper.Emulator.Settings
{
    public interface IAdStatesPatternBase
    {
        AdStatesPattern GetPattern(string placeSettingsPattern);
    }
}