using System.Collections.Generic;
using Core.Utils;
using Core.Utils.TriggerCollision;
using Ecs.Utils;
using Ecs.Views.Linkable.Impl.Modules.Obstacles.Jobs;
using Game.Utils;
using Generated.Commands;
using JCMG.EntitasRedux.Commands;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.Obstacles
{
    public class ExplosionEffectModule : AObjectViewModule
    {
        [SerializeField]
        private Color _triggersColor;

        [SerializeField]
        private Box[] _explosionEffectTriggers;

        private readonly Stack<int> _triggersUids = new Stack<int>();

        private ICommandBuffer _commandBuffer;
        private IPlayerBoxTracker _playerBoxTracker;

        [Inject]
        public void Construct(ICommandBuffer commandBuffer, IPlayerBoxTracker playerBoxTracker)
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

        public override void DrawGizmos()
        {
            // Более показать не могу
        }
    }
}