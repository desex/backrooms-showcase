using Animancer;
using Db.SoundFxBase;
using Ecs.Utils;
using FMODUnity;
using Game.Services.Sound;
using Game.Utils;
using Game.Utils.Animancer;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using R3;
using R3.Triggers;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.AnimancerModules
{
    public class TrainObstacleAnimancerModule : AObjectViewModule
    {
        [Required]
        [SerializeField]
        private SphereCollider _changeAnimationTrigger;
        
        [Required]
        [SerializeField]
        private GameObject _obstacleColliderObject;
        
        [Required]
        [SerializeField]
        private ETrainType _trainType;
        
        [Required]
        [SerializeField]
        private AnimancerComponent _animancer;
        
        [SerializeField]
        private float _defaultTriggerRadius;
        
        [Space]
        [Header("Animation Clips")]
        [SerializeField]
        private MotionClipTransition _train;
        
        [SerializeField]
        private MotionClipTransition _trainStop;
        
        [SerializeField]
        private MotionClipTransition _trainLeaving;
        
        private GameContext _game;
        private AnimancerState _trainState;
        private StudioEventEmitter _trainArrival;
        private StudioEventEmitter _trainDoorsOpen;
        
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
        
        public override void Activate()
        {
            // Более показать не могу
        }
        
        private void ChangeColliderSize()
        {
            // Более показать не могу
        }
        
        private void ActivateCollider()
        {
            // Более показать не могу
        }
        
        private void SetUpEmitter(StudioEventEmitter emitter)
        {
            // Более показать не могу
        }
        
        private void OnAnimationTriggerEntered(Collider other)
        {
            // Более показать не могу
        }
        
        private void SubscribeChangeAnimation(MotionClipTransition animation)
        {
            // Более показать не могу
        }
        
        private void OnPlayAnimationOpenDoors()
        {
            // Более показать не могу
        }
        
        private void OnPlayAnimationTrainArrival()
        {
            // Более показать не могу
        }
    }
}