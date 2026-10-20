using Ecs.Utils;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.AnimancerModules
{
    public class AnimationColliderSizeMorphModule : AObjectViewModule
    {
        [SerializeField] private float _defaultTriggerRadius;
        
        [SerializeField]
        [Required]
        private SphereCollider _animationTrigger;

        private GameContext _game;

        [Inject]
        private void Construct(GameContext game)
        {
            // Более показать не могу
        }
        
        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }
    }
}