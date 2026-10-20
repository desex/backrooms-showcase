using Ecs.Utils;
using Game.Services.Lights;
using Game.Utils;
using Game.Utils.Lights;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using R3;
using R3.Triggers;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.Transit
{
    public class ResetLightOnLevelExitModule : AObjectViewModule
    {
        [SerializeField] private Collider _exitCollider;
        
        private ILightService _lightService;

        private bool _wasEnterHandled;

        [Inject]
        private void Construct(ILightService lightService)
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
        
        private void OnNearExitCollider(Collider trigger)
        {
            // Более показать не могу
        }
    }
}