using Ecs.Utils;
using Game.Utils;
using Generated.Commands;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;
using JCMG.EntitasRedux.Core.Utils;
using R3;
using R3.Triggers;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules
{
    public class ChunkCenterModule : AObjectViewModule
    {
        [SerializeField] private Transform _chunkCenter;
        [SerializeField] private BoxCollider _collider;
        
        private ICommandBuffer _commandBuffer;
        private ILinkedEntityRepository _linkedEntityRepository;

        [Inject]
        private void Construct(
            ICommandBuffer commandBuffer, 
            ILinkedEntityRepository linkedEntityRepository
        )
        {
            // Более показать не могу
        }
        
        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }
        
        private void OnTriggerEntered(Collider other)
        {
            // Более показать не могу
        }
    }
}