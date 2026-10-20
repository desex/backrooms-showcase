using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Db.Monsters.Impl
{
    [CreateAssetMenu(menuName = "Settings/Chaser/" + nameof(ChaserPresentationParameters), fileName = nameof(ChaserPresentationParameters))]
    public class ChaserPresentationParameters : ScriptableObject, IChaserPresentationParameters
    {
        [field: SuffixLabel("seconds", Overlay = true)]
        [field: SerializeField] public float ShowedMonsterDelay { get; private set; } = 1f;

        [field: SuffixLabel("seconds", Overlay = true)]
        [field: SerializeField] public float AfterTutorialShowedMonsterDelay { get; private set; } = 3f;
        
        [field: SuffixLabel("seconds", Overlay = true)]
        [field: SerializeField] public float LookingBackAnimationDelay { get; private set; } = 1f;

        [field: SuffixLabel("seconds", Overlay = true)]
        [field: SerializeField] public float MoveToPlayerDelay { get; private set; } = 1f;
        
        [field: SuffixLabel("seconds", Overlay = true)]
        [field: SerializeField] public float MoveDuration { get; private set; } = 3f;
        
        [field: SerializeField] public float StartDistance { get; private set; } = 5f;
        [field: SerializeField] public float MinDistance { get; private set; } = 2f;
        [field: SerializeField] public float TargetDistance { get; private set; } = 3f;
        [field: SerializeField] public Ease MoveEase { get; private set; }
        [field: SerializeField] public float DefaultAnimationSpeed { get; private set; } = 1f;
        [field: SerializeField] public float DashAnimationSpeed { get; private set; } = 1.5f;
    }
}