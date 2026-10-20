using Core.Utils.TriggerCollision;
using Ecs.Utils;
using Ecs.Views.Linkable.Impl.Modules.Obstacles.Jobs;
using Game.Utils;
using Generated.Commands;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;
using JCMG.EntitasRedux.Core.Utils;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.Obstacles
{
    public partial class PipeReactionBerserkModule : AObjectViewModule
    {
        [SerializeField]
        private Box _trigger;

        [SerializeField]
        private MeshRenderer _colorPicker;

        private ICommandBuffer _commandBuffer;
        private MaterialPropertyBlock _materialPropertyBlock;
        private IPlayerBoxTracker _playerBoxTracker;
        private int _triggerUid;

        private void Start()
        {
            // Более показать не могу
        }

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

        private void LinkBoxActions()
        {
            // Более показать не могу
        }

        private void UnlinkBoxActions()
        {
            // Более показать не могу
        }

        private void OnReactionBerserk()
        {
            // Более показать не могу
        }

        private void OnStretchSize(GameEntity entity, float stretchSize)
        {
            // Более показать не могу
        }

        public override void Deactivate()
        {
            // Более показать не могу
        }
    }
}