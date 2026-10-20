using Animancer;
using Db.SoundFxBase;
using Ecs.Utils;
using FMODUnity;
using Game.Services.Sound;
using Game.Utils;
using Game.Utils.Animancer;
using Game.Utils.Sound.SoundParametersKey;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using R3;
using R3.Triggers;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.AnimancerModules
{
    public class FallScientistObstacleModule : AObjectViewModule
    {
        [Required]
        [SerializeField] private SphereCollider _animationTrigger;

        [SerializeField] private float _defaultAnimationTriggerRadius;

        [Space(20)]
        [SerializeField] private MotionClipTransition _ceilingTransition;

        [SerializeField] private MotionClipTransition _scientistTransition;

        [Space(20)]
        [SerializeField] private AnimancerComponent _ceilingAnimancer;

        [SerializeField] private AnimancerComponent _scientistAnimancer;

        private GameContext _game;
        private StudioEventEmitter _ceilingFallEmitter;
        private StudioEventEmitter _landingFallEmitter;

        [Inject]
        public void Construct(
            GameContext game,
            ISoundService soundService
        )
        {
            // Более показать не могу
        }

        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }
        
        private void SetUpEmitter(StudioEventEmitter emitter)
        {
            // Более показать не могу
        }

        private void OnTriggerEntered(Collider other)
        {
            // Более показать не могу
        }

        private void OnPlayScientistAnimation()
        {
            // Более показать не могу
        }

        private void OnCellAnimationPlay()
        {
            // Более показать не могу
        }
    }
}