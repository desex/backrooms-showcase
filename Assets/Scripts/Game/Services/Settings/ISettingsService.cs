
using R3;

namespace Game.Services.Settings
{
    public interface ISettingsService
    {
        ReadOnlyReactiveProperty<bool> IsNotVolumeMuted { get; }
        ReadOnlyReactiveProperty<float> Sensitivity { get; }
        ReadOnlyReactiveProperty<bool> IsVibrationEnabled { get; }

        void SetSensitivity(float sensitivity);
        void Mute();
        void Unmute();
        void EnableVibration();
        void DisableVibration();
    }
}