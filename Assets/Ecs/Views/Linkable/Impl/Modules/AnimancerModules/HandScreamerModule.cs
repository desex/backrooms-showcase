using System;
using Animancer;
using Db.SoundFxBase;
using Ecs.Utils;
using FMODUnity;
using Game.Services.Analytics;
using Game.Services.Sound;
using Game.Utils;
using Game.Utils.Animancer;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using R3;
using R3.Triggers;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.AnimancerModules
{
    public class HandScreamerModule : AObjectViewModule
    {
        [Header("Animations")]
        [SerializeField] private AnimancerComponent _animancer;
        [SerializeField] private MotionClipTransition _armAnimation;
        [SerializeField] private Transform _soundTransform;
        
        [Space(10)]
        [Header("Trigger")]
        [SerializeField] private Collider _animationTrigger;
        
        private IDisposable _disposable;
        private StudioEventEmitter _emitter;
        private IAnalyticsService _analyticsService;

        [Inject]
        public void Construct(
            ISoundService soundService,
            IAnalyticsService analyticsService
        )
        {
            // Более показать не могу
        }
        
        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }

        private void OnPlaySound(int key)
        {
            // Более показать не могу
        }

        private void PlayHandAnimation(Collider other)
        {
            // Более показать не могу
        }

        private void OnEndHandAnimation()
        {
            // Более показать не могу
        }
    }
}