using System;
using System.Threading;
using Animancer;
using Cysharp.Threading.Tasks;
using Db.Screamers;
using Ecs.Utils;
using Game.Services.Analytics;
using Game.Utils;
using Game.Utils.Animancer;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using Sirenix.OdinInspector;
using R3;
using R3.Triggers;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.AnimancerModules
{
    public class MonsterScreamerAnimancerModule : AObjectViewModule
    {
        [Header("Triggers")]
        [Required] 
        [SerializeField] 
        private BoxCollider _playAnimationTrigger;

        [Header("Animancer")]
        [Required] 
        [SerializeField] 
        private AnimancerComponent _animancer;
        
        [Header("Animation Clips")]
        [SerializeField] private MotionClipTransition _monsterScreamerRunOut;
        [SerializeField] private MotionClipTransition _monsterScreamerRunIn;
        [SerializeField] private MotionClipTransition _monsterScreamerIdle;
        
        [Header("Points")]
        [SerializeField] private Transform _inTransform;
        [SerializeField] private Transform _outTransform;
        
        [Header("Monster")]
        [SerializeField] private Transform _monsterTransform;
        
        private IMonsterScreamerParameters _monsterScreamerParameters;
        private IAnalyticsService _analyticsService;
        private CancellationTokenSource _cancellationToken;
        private TimeSpan _idleDuration;

        [Inject]
        private void Construct(
            IMonsterScreamerParameters monsterScreamerParameters, 
            IAnalyticsService analyticsService
        )
        {
            // Более показать не могу
        }

        private void OnDisable()
        {
            // Более показать не могу
        }

        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }
        
        private void OnAnimationTriggerEntered(Collider other)
        {
            // Более показать не могу
        }

        private async void Play(CancellationToken token)
        {
            // Более показать не могу
        }

        private async UniTask Move(Vector3 to, CancellationToken token)
        {
            // Более показать не могу
        }
    }
}