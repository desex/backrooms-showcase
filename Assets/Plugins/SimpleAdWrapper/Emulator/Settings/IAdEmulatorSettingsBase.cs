using Plugins.SimpleAdWrapper.Emulator.Settings.Impl;

namespace Plugins.SimpleAdWrapper.Emulator.Settings
{
    public interface IAdEmulatorSettingsBase
    {
        AdPreloadPattern PreloadPattern { get; }
        
        AdEmulatorPlace GetPlace(string place, EAdType adType);
    }
}