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
using PdUtils.RandomProvider;
using R3;
using R3.Triggers;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Screamer
{
    public class BodyScreamerModule : AObjectViewModule
    {
        [SerializeField] private AnimancerComponent[] _screamersAnimancer;
        [SerializeField] private Collider _animationTrigger;
        [SerializeField] private MotionClipTransition _screamerAnimation;
        [SerializeField] private MotionClipTransition _idleAnimation;

        private IAnalyticsService _analyticsService;
        private IRandomProvider _randomProvider;
        private IDisposable _bodyScreamerAnimationEndDisposable;
        private IDisposable _triggerEnterDisposable;
        private AnimancerComponent _chosenAnimancer;
        private StudioEventEmitter _bodyDraggingEmitter;

        [Inject]
        private void Construct(
            IRandomProvider randomProvider, 
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

        private void ActivateScreamer(Collider other)
        {
            // Более показать не могу
        }

        private void OnEndBodyPull()
        {
            // Более показать не могу
        }

        private void ChangeScreamerVisibility(bool visibility)
        {
            // Более показать не могу
        }

        private void SetUpEmitter()
        {
            // Более показать не могу
        }
    }
}