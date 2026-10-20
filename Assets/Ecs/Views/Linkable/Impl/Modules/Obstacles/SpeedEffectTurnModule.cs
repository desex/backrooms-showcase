using Core.Utils.TriggerCollision;
using Ecs.Utils;
using Ecs.Views.Linkable.Impl.Modules.Obstacles.Jobs;
using Game.Utils;
using Game.Utils.Movement;
using Generated.Commands;
using JCMG.EntitasRedux.Commands;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.Obstacles
{
    public class SpeedEffectTurnModule : AObjectViewModule
    {
        [SerializeField]
        private Box _trigger;

        [SerializeField]
        private EObstacleTypeForAcceleration _obstacleTypeForAcceleration;

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

        private void OnTriggerSpeedEffectEnter()
        {
            // Более показать не могу
        }
    }
}