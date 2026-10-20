using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Db.Death.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(DeadBlackoutParameters), fileName = nameof(DeadBlackoutParameters))]
    public class DeadBlackoutParameters : SerializedScriptableObject, IDeadBlackoutParameters
    {
        [field: SerializeField] public float FadeDuration { get; private set; } = 0.6f;
        [field: SerializeField] public Ease FadeEase { get; private set; } = Ease.Linear;
    }
}