using Animancer;
using Db.Player;
using Ecs.Utils;
using Game.Utils.Animancer;
using Game.Utils.Chunks;
using Game.Utils.Movement;
using Game.Utils.Rebound;
using Game.Utils.Slides;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using PdUtils.RandomProvider;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.AnimancerModules
{
    public class ControllableSlideModule : AObjectViewModule
    {
        [Header("Move")]
        [SerializeField] private MotionClipTransitionSequence _transitionToLeft;
        [SerializeField] private MotionClipTransitionSequence _transitionToRight;
        
        [Header("Damage")]
        [SerializeField] private MotionClipTransition _leftDamage;
        [SerializeField] private MotionClipTransition _rightDamage;
        [SerializeField] private MotionClipTransition _backDamage;
        
        private IRandomProvider _randomProvider;
        private IPlayerSlideAndJumpParameters _slideAndJumpParameters;
        private GameContext _game;
        private AnimancerComponent _animancer;
        private GameEntity _self;

        [Inject]
        private void Construct(
            IRandomProvider randomProvider,
            GameContext game,
            IPlayerSlideAndJumpParameters slideAndJumpParameters
            )
        {
            // Более показать не могу
        }
        
        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }

        private void OnDirection(GameEntity entity, EReboundDirection value)
        {
            // Более показать не могу
        }

        private bool CanPlayRebound(GameEntity entity)
        {
            // Более показать не могу
            return default;
        }

        private void OnBodyMoveType(GameEntity entity, EBodyMoveType value)
        {
            // Более показать не могу
        }

        private void OnControllableSlideDirection(GameEntity entity, float value)
        {
            // Более показать не могу
        }

        private bool CanPlayLeft(float value)
        {
            // Более показать не могу
            return default;
        }
        
        private bool CanPlayRight(float value)
        {
            // Более показать не могу
            return default;
        }

        private bool IsSequencePlaying(MotionClipTransitionSequence sequence)
        {
            // Более показать не могу
            return default;
        }

        private bool IsPlayingClip(AnimationClip clip) {
            // Более показать не могу
            return default;
        }
        
        private ITransition GetReboundTransition(EReboundDirection value)
        {
            // Более показать не могу
            return default;
        }

        private ESlideSide GetSlideSide()
        {
            // Более показать не могу
            return default;
        }
        
        private ITransition GetForwardTransition(ESlideSide slideSide)
        {
            // Более показать не могу
            return default;
        }
    }
}