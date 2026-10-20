using System.Collections.Generic;
using Ecs.Utils;
using Game.Utils.Collisions;
using Game.Utils.Rebound;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.Rebound
{
    public class ReboundModule : AObjectViewModule
    {
        private ICollisionService _collisionService;
        
        [FormerlySerializedAs("_setups")]
        [SerializeField]
        private List<ReboundSettings> _settings;

        [Inject]
        private void Construct(ICollisionService collisionService)
        {
            // Более показать не могу
        }

        public override void Subscribe(
            IEntity entity,
            ObjectView objectView,
            IUnsubscribeEvent unsubscribe
        )
        {
            foreach (var settings in _settings)
                foreach (var setupCollider in settings.Colliders)
                {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
                    if (!setupCollider)
                        Debug.LogError(nameof(ReboundModule) + "Collider is null at " + gameObject.name);
#endif

                    Register(setupCollider, settings);
                }
        }

        private void Register(Collider setupCollider, ReboundSettings settings)
        {
            // Более показать не могу
        }
    }
}