using UnityEngine;

namespace Db.Effects.Impl
{
    [CreateAssetMenu(menuName = "Settings/UI/" + nameof(AccelerationFeedbackParameters), fileName = nameof(AccelerationFeedbackParameters))]
    public class AccelerationFeedbackParameters : ScriptableObject, IAccelerationFeedbackParameters
    {
        [field: SerializeField] public float AppearDuration { get; private set; } = 0.5f;
        [field: SerializeField] public float DisappearDuration { get; private set; } = 0.6f;
        [field: SerializeField] public float MaxAlpha { get; private set; } = 1f;
        [field: SerializeField] public float MinAlpha { get; private set; } = 0f;
    }
}