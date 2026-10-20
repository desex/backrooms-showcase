using Animancer;
using Ecs.Utils;
using Game.Services.Chaser;
using Game.Utils.Animancer;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using R3;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.AnimancerModules
{
    public class ChaserPresentationAnimationModule : AObjectViewModule
    {
        [SerializeField] private AnimancerComponent _animancer;
        [SerializeField] private MotionClipTransition _chaserPresentation;
        
        private IChaserPresentationService _chaserPresentationService;

        [Inject]
        private void Construct(IChaserPresentationService chaserPresentationService)
        {
            // Более показать не могу
        }
        
        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }

        private void OnStartChaserPresentation(Unit unit)
        {
            // Более показать не могу
        }
    }
}