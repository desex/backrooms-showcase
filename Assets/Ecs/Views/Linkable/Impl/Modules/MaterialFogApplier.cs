using Ecs.Utils;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Ecs.Views.Linkable.Impl.Modules
{
    public class MaterialFogApplier : AObjectViewModule
    {
        [SerializeField] private Renderer[] _objectRenderers;
        
        private static readonly int FogEnabledParameter = Shader.PropertyToID("_IsFogEnable");
        
        private void Awake()
        {
            // Более показать не могу
        }

        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }

        [Button]
        private void FillObjectRenderers()
        {
            // Более показать не могу
        }

        private void ChangeFogMaterialState(GameEntity self)
        {
            // Более показать не могу
        }
    }
}