using System.Collections.Generic;
using Core.Utils.TriggerCollision;
using Ecs.Utils;
using Ecs.Views.Linkable.Impl.Modules.Obstacles.Jobs;
using Game.Utils;
using Game.Utils.Lights;
using Generated.Commands;
using JCMG.EntitasRedux.Commands;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.Lights
{
    public partial class LightEventModule : AObjectViewModule
    {
        [Required]
        [SerializeField]
        private Box _trigger;

        [SerializeField]
        private List<ELightEvent> _lightEvents;

        private ICommandBuffer _commandBuffer;
        private IPlayerBoxTracker _playerBoxTracker;
        private int _triggerUid;

        [Inject]
        private void Construct(ICommandBuffer commandBuffer, IPlayerBoxTracker playerBoxTracker)
        {
            // Более показать не могу
        }

        public override void Activate()
        {
            // Более показать не могу
        }

        public override void Deactivate()
        {
            // Более показать не могу
        }
    }
}