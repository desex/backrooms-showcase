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

namespace Ecs.Views.Linkable.Impl.Obstacles.DynamicObstacles
{
    public class HandCellAnimationModule : AObjectViewModule
    {
        [Required]
        [SerializeField]
        private SphereCollider _animationTrigger;

        [SerializeField]
        private GameObject _shadow;

        [SerializeField]
        private float _defaultAnimationTriggerRadius;

        [Required]
        [SerializeField]
        private SphereCollider _screamerHandTrigger;

        [SerializeField]
        private float _defaultScreamerHandTriggerRadius;

        [SerializeField]
        private AnimancerComponent _cellAnimancer;

        [SerializeField]
        private MotionClipTransition _cellsDropAnimation;

        private GameContext _game;
        private StudioEventEmitter _screamMonsterEmitter;
        private StudioEventEmitter _ceilingFallEmitter;

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

        private void OnDropCells(Collider other)
        {
            // Более показать не могу
        }

        private void OnPlayScreamer(Collider other)
        {
            // Более показать не могу
        }

        private void OnCellAnimationPlay()
        {
            // Более показать не могу
        }
    }
}