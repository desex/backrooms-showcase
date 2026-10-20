using Ecs.Utils;
using Game.Services.CollectablesItem;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.Collectables
{
    public class CollectablesSpawnModule : AObjectViewModule
    {
        [SerializeField] private Transform[] _collectableSpawnPoint;
        
        private ICollectableSpawnService _collectableSpawnService;

        [Inject]
        private void Construct(ICollectableSpawnService collectableSpawnService)
        {
            // Более показать не могу
        }

        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }

        private void OnSetNewSpawnPosition(GameEntity entity, Transform value)
        {
            // Более показать не могу
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            // Более показать не могу
        }
#endif
    }
}