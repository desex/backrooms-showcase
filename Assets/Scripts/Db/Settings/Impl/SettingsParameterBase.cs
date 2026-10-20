using UnityEngine;

namespace Db.Settings.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(SettingsParameterBase), fileName = nameof(SettingsParameterBase))]
    public sealed class SettingsParameterBase : ScriptableObject, ISettingsParameter
    {
        [field: SerializeField] public SensitivityParameterVo Sensitivity { get; private set; }
        [field: SerializeField] public bool SoundDefaultState { get; private set; }
        [field: SerializeField] public bool VibrationDefaultState { get; private set; }
    }
}