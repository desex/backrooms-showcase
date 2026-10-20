namespace Db.Settings
{
    public interface ISettingsParameter
    {
        SensitivityParameterVo Sensitivity { get; }
        bool SoundDefaultState { get; }
        bool VibrationDefaultState { get; }
    }
}