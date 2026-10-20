using System.Collections.Generic;
using Ecs.Utils;
using Ecs.Views.Linkable.Impl.Obstacles;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Ecs.Views.Linkable.Impl.Modules.Revive
{
    public class ReviveObjectHiderModule : AObjectViewModule
    {
        [field: SerializeField]
        public bool HasWalls { get; private set; }
        public GameEntity Entity { get; private set; }

        [field: ShowIf(nameof(HasWalls))]
        [field: Required]
        [field: SerializeField]
        public List<GameObject> GameObjects { get; private set; }

        [field: HideIf(nameof(HasWalls))]
        [field: SerializeField]
        public ObstacleGroupView ObstacleGroupView { get; private set; }

        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }

        public override void Activate()
        {
            // Более показать не могу
        }

        public void DisableObjects()
        {
            // Более показать не могу
        }

        private void ActivateObjects()
        {
            // Более показать не могу
        }

# if UNITY_EDITOR
        [Button]
        public void Fill()
        {
            // Более показать не могу
        }
# endif
    }
}