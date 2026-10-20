using Ecs.Utils;
using Game.Utils;
using Game.Utils.Ghost;
using Generated.Commands;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;
using JCMG.EntitasRedux.Core.Utils;
using R3;
using R3.Triggers;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.Obstacles
{
    public class GhostAutomaticJumpTriggerModule : AObjectViewModule
    {
        [SerializeField] private EGhostSimpleJumpType _simpleJumpType;
        [SerializeField] private Collider[] _jumpColliders;
        
        private ILinkedEntityRepository _linkedEntityRepository;
        private ICommandBuffer _commandBuffer;

        [Inject]
        private void Construct(
            ILinkedEntityRepository linkedEntityRepository, 
            ICommandBuffer commandBuffer
        )
        {
            // Более показать не могу
        }

        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }

        private void OnJumpTriggerEnterAsObservable(Collider other)
        {
            // Более показать не могу
        }
    }
}