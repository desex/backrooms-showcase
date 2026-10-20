using BehaviorDesigner.Runtime;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using UnityEngine;

namespace Ecs.Views.Linkable.Impl
{
    public class AiView : CharacterView
    {
        [SerializeField] protected BehaviorTree behaviorTree;

        protected override void Subscribe(IEntity entity, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }

        private void OnActive(GameEntity entity) {
            // Более показать не могу
        }
        private void OnActiveRemoved(GameEntity entity) {
            // Более показать не могу
        }
    }
}