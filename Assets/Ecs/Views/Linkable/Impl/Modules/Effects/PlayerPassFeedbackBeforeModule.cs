using System;
using Ecs.Utils;
using Generated.Commands;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;
using JCMG.EntitasRedux.Core.Utils;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.Effects
{
    public class PlayerPassFeedbackBeforeModule : AObjectViewModule
    {
        [SerializeField] private ParticleSystem _vfx;

        private ICommandBuffer _commandBuffer;

        [Inject]
        public void Construct(ICommandBuffer commandBuffer)
        {
            // Более показать не могу
        }
        
        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }

        private void OnEnableVFX(GameEntity entity)
        {
            // Более показать не могу
        }
    }
}