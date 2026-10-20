using Ecs.Utils;
using Ecs.Views.Linkable.Impl.Collectables;
using Game.Utils;
using Generated.Commands;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;
using JCMG.EntitasRedux.Core.Utils;
using R3;
using R3.Triggers;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.Collectables
{
    public class CollectiblesSelectionModule : AObjectViewModule
    {
        [SerializeField] private Collider _selectionTrigger;
        
        private ICommandBuffer _commandBuffer;

        [Inject]
        private void Construct(ICommandBuffer commandBuffer)
        {
            // Более показать не могу
        }
        
        
        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }

        private void OnSelect(Collider trigger)
        {
            // Более показать не могу
        }
    }
}