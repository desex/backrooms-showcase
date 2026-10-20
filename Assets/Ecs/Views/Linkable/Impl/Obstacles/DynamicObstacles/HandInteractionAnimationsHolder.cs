using Animancer;
using Game.Utils.Animancer;
using R3;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Obstacles.DynamicObstacles
{
    public class HandInteractionAnimationsHolder : MonoBehaviour
    {
        private const string QTE_ANIMATION_FOLDOUT = "QTE Animations";
        private const string HAND_ANIMATION_FOLDOUT = "Hand Animations";
        private const string CEILING_ANIMATION_FOLDOUT = "Ceiling Animations";
        private const string PLAYER_ANIMATION_FOLDOUT = "Player Animations";
        private const string ANIMANCERS_FOLDOUT = "Animancers";

        [FoldoutGroup(ANIMANCERS_FOLDOUT)]
        [SerializeField]
        private AnimancerComponent _ceilingAnimancer;

        [FoldoutGroup(ANIMANCERS_FOLDOUT)]
        [SerializeField]
        private AnimancerComponent _handAnimancer;

        [FoldoutGroup(CEILING_ANIMATION_FOLDOUT)]
        [SerializeField]
        private MotionClipTransition _ceilingFall;

        [FoldoutGroup(HAND_ANIMATION_FOLDOUT)]
        [SerializeField]
        private MotionClipTransition _handAppereance;

        [FoldoutGroup(HAND_ANIMATION_FOLDOUT)]
        [SerializeField]
        private MotionClipTransition _handSearch;

        [Space]
        [FoldoutGroup(QTE_ANIMATION_FOLDOUT)]
        [SerializeField]
        private MotionClipTransition _handCatch;

        [FoldoutGroup(QTE_ANIMATION_FOLDOUT)]
        [SerializeField]
        private MotionClipTransition _handCatchPunch;

        [FoldoutGroup(QTE_ANIMATION_FOLDOUT)]
        [SerializeField]
        private MotionClipTransition _handCatchPullingOut;

        [Space]
        [FoldoutGroup(PLAYER_ANIMATION_FOLDOUT)]
        [SerializeField]
        private MotionClipTransition _playerHandCatchStart;

        [FoldoutGroup(PLAYER_ANIMATION_FOLDOUT)]
        [SerializeField]
        private MotionClipTransition _playerHandCatchLoop;

        [FoldoutGroup(PLAYER_ANIMATION_FOLDOUT)]
        [SerializeField]
        private MotionClipTransition _playerHandCatchPunch;

        [FoldoutGroup(PLAYER_ANIMATION_FOLDOUT)]
        [SerializeField]
        private MotionClipTransition _playerHandCatchPullingOut;

        [FoldoutGroup(PLAYER_ANIMATION_FOLDOUT)]
        [SerializeField]
        private MotionClipTransition _playerDeath;

        private GameContext _game;

        public MotionClipTransition HandCatch => _handCatch;

        public Observable<Unit> HandAppeared => Observable.FromEvent(
            action => _handAppereance.Events.OnEnd += action,
            action => _handAppereance.Events.OnEnd -= action
        );

        public Observable<Unit> EndKillAnimation => Observable.FromEvent(
            action => _handCatch.Events.OnEnd += action,
            action => _handCatch.Events.OnEnd -= action
        );

        public Observable<Unit> PunchAnimationEnd => Observable.FromEvent(
            action => _playerHandCatchPunch.Events.OnEnd += action,
            action => _playerHandCatchPunch.Events.OnEnd -= action
        );

        public Observable<Unit> EndQteEscapeAnimation => Observable.FromEvent(
            action => _playerHandCatchPullingOut.Events.OnEnd += action,
            action => _playerHandCatchPullingOut.Events.OnEnd -= action
        );

        public Observable<Unit> DeactivateHand => Observable.FromEvent(
            action => _handCatchPullingOut.Events.OnEnd += action,
            action => _handCatchPullingOut.Events.OnEnd -= action
        );

        [Inject]
        public void Construct(GameContext game)
        {
            // Более показать не могу
        }

        public void OnPauseAnimancer(GameEntity entity, bool value)
        {
            // Более показать не могу
        }

        public void PlayHandSearch()
        {
            // Более показать не могу
        }

        public void PlayCeilingFall()
        {
            // Более показать не могу
        }

        public bool OnHandCatchPunch()
        {
            // Более показать не могу
            return default;
        }

        public void PlayHandCatchAnimation()
        {
            // Более показать не могу
        }

        public void PlayPunchAnimation()
        {
            // Более показать не могу
        }

        public void PlayHandCatchPullingOutAnimation()
        {
            // Более показать не могу
        }
    }
}