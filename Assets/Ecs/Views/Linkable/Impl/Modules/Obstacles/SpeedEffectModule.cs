using Ecs.Utils;
using Game.Utils;
using Generated.Commands;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;
using JCMG.EntitasRedux.Core.Utils;
using R3;
using R3.Triggers;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.Obstacles
{
    public class SpeedEffectModule : AObjectViewModule
    {
        [field: SerializeField] private BoxCollider _obstacleStartTrigger;
        [field: SerializeField] private BoxCollider _obstacleEndTrigger;
        [field: SerializeField] private ESpeedEffectAxis _speedEffectAxis;
        
        private GameContext _gameContext;
        private ICommandBuffer _commandBuffer;

        [Inject]
        private void Construct(GameContext gameContext, ICommandBuffer commandBuffer)
        {
            // Более показать не могу
        }
        
        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }

        private void OnTriggerSpeedEffectEnter(Collider trigger)
        {
            // Более показать не могу
        }
        
        private void OnTriggerSpeedEffectExit(Collider trigger)
        {
            // Более показать не могу
        }
    }
}