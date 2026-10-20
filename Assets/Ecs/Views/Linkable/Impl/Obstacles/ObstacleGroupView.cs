using System.Collections.Generic;
using System.Linq;
using Ecs.Utils;
using Game.Utils;
using Game.Utils.Obstacles;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Ecs.Views.Linkable.Impl.Obstacles
{
    public class ObstacleGroupView : PoolObjectView, IPreInitializable
    {
        protected const string GENERAL_GROUP = "General";

        [TabGroup(GENERAL_GROUP)]
        public EObstacleType ObstacleType;

        [field: TabGroup(GENERAL_GROUP)] 
        [field: SerializeField]
        public bool IsOffsettable { get; private set; }

        [field: TabGroup(GENERAL_GROUP)]
        [field: SerializeField] 
        public List<ObstacleView> ChildrenObstacles { get; private set; }

        [field: TabGroup(GENERAL_GROUP)]
        [field: SerializeField]
        public List<ObstacleGroupView> ChildrenObstacleGroups { get; private set; }

        [field: TabGroup(GENERAL_GROUP)]
        [field: SerializeField] 
        public bool IsKillingInstantly { get; private set; }

        [field: TabGroup(GENERAL_GROUP)]
        [field: SerializeField]
        public Transform EndPoint { get; private set; }

        [field: TabGroup(GENERAL_GROUP)]
        [field: SerializeField]
        private AObjectViewModule _drawModule;

        [Button("AutoFill")]
        public void PreInitialize()
        {
            // Более показать не могу
        }
        
        private void FillChildrenObstacles()
        {
            // Более показать не могу
        }
        
        private void FillChildrenGroups()
        {
            // Более показать не могу
        }

        private void DeleteChildrenObstaclesIfNested()
        {
            // Более показать не могу
        }

#if UNITY_EDITOR
        protected virtual void OnDrawGizmosSelected()
        {
            // Более показать не могу
        }
#endif
    }
}