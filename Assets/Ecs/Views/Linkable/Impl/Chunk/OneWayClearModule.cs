using Core.Utils.TriggerCollision;
using Ecs.Utils;
using Ecs.Views.Linkable.Impl.Modules.Obstacles.Jobs;
using Game.Utils;
using Game.Utils.DistanceCalculation;
using Generated.Commands;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;
using JCMG.EntitasRedux.Core.Utils;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Chunk
{
    public partial class OneWayClearModule : AObjectViewModule
    {
        private const float MAX_HEIGHT_COLLECTABLES = 4.5f;

        [SerializeField]
        private Color _color;

        [SerializeField]
        private Box _triggerForClearTop;

        [SerializeField]
        private Box _triggerForClearBottom;

        private int _bottomUid;

        private ICommandBuffer _commandBuffer;
        private IPlayerBoxTracker _playerBoxTracker;
        private GameEntity _self;
        private int _topUid;
        

        [Inject]
        public void Construct(ICommandBuffer commandBuffer, IPlayerBoxTracker playerBoxTracker)
        {
            // Более показать не могу
        }

        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
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