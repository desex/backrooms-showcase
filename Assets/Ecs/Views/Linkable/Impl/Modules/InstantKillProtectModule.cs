using Ecs.Utils;
using Game.Utils;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using R3;
using R3.Triggers;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules
{
    public class InstantKillProtectModule : AObjectViewModule
    {
        [Inject] private readonly GameContext _game;
        
        [SerializeField] private Collider _turnTrigger;
        
        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }

        private void OnProtectionTriggerEntered(Collider other)
        {
            // Более показать не могу
        }

        private void OnProtectionTriggerExit(Collider other)
        {
            // Более показать не могу
        }

        private void ChangeProtectionState(Collider other, bool isTurning)
        {
            // Более показать не могу
        }
    }
}