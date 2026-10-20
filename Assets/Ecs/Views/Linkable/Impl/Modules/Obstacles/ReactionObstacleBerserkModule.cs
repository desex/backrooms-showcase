using System.Collections.Generic;
using Core.Utils.TriggerCollision;
using Ecs.Utils;
using Ecs.Views.Linkable.Impl.Modules.Obstacles.Jobs;
using Game.Utils;
using Game.Utils.BerserkReaction;
using Generated.Commands;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;
using JCMG.EntitasRedux.Core.Utils;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.Obstacles
{
    public partial class ReactionObstacleBerserkModule : AObjectViewModule
    {
        [ShowIf("_shouldDisabledObject")]
        [SerializeField]
        private List<GameObject> _objectsToDisable;
        
        [Space]
        [SerializeField]
        private List<BerserkReactionEffect> _effects;

        [Space]
        [SerializeField]
        private Color _triggerColor = Color.blue;

        [SerializeField]
        private Box _trigger;

        private ICommandBuffer _commandBuffer;
        private IPlayerBoxTracker _playerBoxTracker;
        private int _triggerUid;

        [Inject]
        private void Construct(ICommandBuffer commandBuffer, IPlayerBoxTracker playerBoxTracker)
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

        private void LinkBoxActions()
        {
            // Более показать не могу
        }

        private void UnlinkBoxActions()
        {
            // Более показать не могу
        }

        private void OnStretchSize(GameEntity entity, float value)
        {
            // Более показать не могу
        }
    }
}