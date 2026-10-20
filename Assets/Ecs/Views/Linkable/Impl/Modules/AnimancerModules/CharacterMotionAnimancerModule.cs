using System.Collections.Generic;
using Ecs.Utils;
using Game.Utils;
using Game.Utils.Analytics;
using Game.Utils.Animancer;
using Game.Utils.Chunks;
using Game.Utils.Movement;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using PdUtils.RandomProvider;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.AnimancerModules
{
    public class CharacterMotionAnimancerModule : AObjectViewModule
    {
        [SerializeField] private MotionClipTransition _landing;
        [SerializeField] private MotionClipTransition _roll;        
        [SerializeField] private MotionClipTransitionSequence _jump;
        [SerializeField] private MotionClipTransition _jumpInObstacle;
        [SerializeField] private MotionClipTransition _jumpInObstacleCut;
        [SerializeField] private MotionClipTransition _swing;
        [SerializeField] private MotionClipTransitionSequence _shortSlide;
        [SerializeField] private MotionClipTransitionSequence _longSlide;
        [SerializeField] private MotionClipTransitionSequence _hitTheObstacle;
        [SerializeField] private List<MotionClipTransition> _monkeyJumpTransitions;
        [SerializeField] private List<MotionClipTransition> _forwardJumpTransitions;
        
        private IRandomProvider _randomProvider;
        
        private readonly Dictionary<int, float> _originSpeedByIdOfAcceleratableClip = new();
        private GameContext _game;

        [Inject]
        private void Construct(
            IRandomProvider randomProvider,
            GameContext game)
        {
            // Более показать не могу
        }
        
        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }

        private void CreateDictionary()
        {
            // Более показать не могу
        }
        
        private MotionClipTransition GetRandomAnimation(IReadOnlyList<MotionClipTransition> motionClips)
        {
            // Более показать не могу
            return default;
        }
        
        private void OnBodyMoveType(GameEntity entity, EBodyMoveType value)
        {
            // Более показать не могу
        }

        private bool IsRumpRebound(GameEntity entity, EBodyMoveType value)
        {
            // Более показать не могу
            return default;
        }
    }
}