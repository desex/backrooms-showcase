using Core.Utils.TriggerCollision;
using Ecs.Utils;
using Ecs.Views.Linkable.Impl.Modules.Obstacles.Jobs;
using Game.Utils;
using Generated.Commands;
using JCMG.EntitasRedux.Commands;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules
{
    public partial class TriggerPlayerKillerModule : AObjectViewModule
    {
        [SerializeField]
        private Box _trigger;

        private ICommandBuffer _commandBuffer;
        private GameContext _game;
        private IPlayerBoxTracker _playerBoxTracker;
        private int _triggerUid;

        [Inject]
        private void Construct(ICommandBuffer commandBuffer, IPlayerBoxTracker playerBoxTracker, GameContext game)
        {
            // Более показать не могу
        }

        public override void Activate()
        {
            // Более показать не могу
        }

        private void KillPlayer()
        {
            // Более показать не могу
        }

        public override void Deactivate()
        {
            // Более показать не могу
        }
    }
}