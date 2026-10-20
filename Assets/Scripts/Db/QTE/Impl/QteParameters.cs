using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Db.QTE.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(QteParameters), fileName = nameof(QteParameters))]
    public class QteParameters : ScriptableObject, IQteParameters
    {
        private const string VIEW_FOLDOUT = "View Parameters";
        private const string HIDE_OBSTACLES_FOLDOUT = "Hide Obstacles Parameters";
        private const string BLACK_SCREEN_FOLDOUT = "Black Screen Parameters";

        [field: SerializeField] public int TapsToEscape { get; private set; } = 5;
        [field: SerializeField] public float TimeForQte { get; private set; } = 3f;

        [field: FoldoutGroup(HIDE_OBSTACLES_FOLDOUT)]
        [field: SerializeField]
        public float BackwardHideObstaclesRadius { get; private set; } = 1f;
        
        [field: FoldoutGroup(HIDE_OBSTACLES_FOLDOUT)]
        [field: SerializeField]
        public float BackwardHideObstaclesPositionOffset { get; private set; } = 1.333f;

        [field: FoldoutGroup(HIDE_OBSTACLES_FOLDOUT)]
        [field: SerializeField]
        public float ForwardHideObstaclesRadius { get; private set; } = 1.5f;

        [field: FoldoutGroup(HIDE_OBSTACLES_FOLDOUT)]
        [field: SerializeField]
        public float ForwardHideObstaclesPositionOffset { get; private set; } = 2f;
        
        [field: SerializeField] public int MaxQteCount { get; private set; } = 3;
        [field: SerializeField] public float TimeDecreasePerSuccess { get; private set; } = 1f;
        [field: SerializeField] public float MaxCorrectHeight { get; private set; } = 0.3f;

        [field: FoldoutGroup(VIEW_FOLDOUT)]
        [field: SerializeField]
        public float CircleAnimationTime { get; private set; } = 0.2f;

        [field: FoldoutGroup(VIEW_FOLDOUT)]
        [field: SerializeField]
        public Ease FillEasing { get; private set; } = Ease.OutExpo;

        [field: FoldoutGroup(VIEW_FOLDOUT)]
        [field: SerializeField]
        public float TextAnimationTime { get; private set; } = 0.4f;

        [field: FoldoutGroup(VIEW_FOLDOUT)]
        [field: SerializeField]
        public Ease TextAnimationEase { get; private set; } = Ease.OutQuad;

        [field: FoldoutGroup(BLACK_SCREEN_FOLDOUT)]
        [field: SerializeField]
        public float BlackoutFadeTime { get; private set; }

        [field: FoldoutGroup(BLACK_SCREEN_FOLDOUT)]
        [field: SerializeField]
        public Ease BlackoutFadeEasing { get; private set; }

        [field: FoldoutGroup(BLACK_SCREEN_FOLDOUT)]
        [field: SerializeField]
        public float BlackoutDelay { get; private set; } = 3f;

    }
}