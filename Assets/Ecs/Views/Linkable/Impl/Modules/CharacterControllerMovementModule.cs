using Ecs.Core.Interfaces;
using Ecs.Utils;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules
{
    public class CharacterControllerMovementModule : AObjectViewModule
    {
        [SerializeField] private CharacterController _characterController;
        
        [Inject] private ITimeProvider _timeProvider;
        
        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }
        
        private void OnVelocity(GameEntity entity, Vector3 value)
        {
            // Более показать не могу
        }
    }
}