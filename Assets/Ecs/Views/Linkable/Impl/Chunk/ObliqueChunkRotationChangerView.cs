using Core.Utils.TriggerCollision;
using Ecs.Utils;
using Ecs.Views.Linkable.Impl.Modules.Obstacles.Jobs;
using Game.Utils;
using Generated.Commands;
using JCMG.EntitasRedux.Commands;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Chunk
{
    public sealed partial class ObliqueChunkRotationChangerView : AObjectViewModule
    {
        [SerializeField]
        private Transform _startTarget;

        [SerializeField]
        private Transform _endTarget;

        [SerializeField]
        private Box _enterPathTrigger;

        [SerializeField]
        private Box _exitPathTrigger;

        private ICommandBuffer _commandBuffer;
        private int _enterTriggerUid;
        private int _exitTriggerUid;
        private GameContext _game;
        private IPlayerBoxTracker _playerBoxTracker;
        
        [Inject]
        private void Construct(ICommandBuffer commandBuffer, GameContext game, IPlayerBoxTracker playerBoxTracker)
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