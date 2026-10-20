using System.Collections.Generic;
using Core.Utils.TriggerCollision;
using Ecs.Managers;
using Ecs.Utils;
using Ecs.Views.Linkable.Impl.Modules.Obstacles.Jobs;
using Game.Utils;
using Game.Utils.Movement;
using Generated.Commands;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;
using JCMG.EntitasRedux.Core.Utils;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.Chunks
{
    public partial class TurnChoiceModule : AObjectViewModule
    {
        [SerializeField]
        private ETurnDirection _turnDirection;

        [SerializeField]
        private Box _turnTrigger;

        [SerializeField]
        private Box _preTurnReactTrigger;

        [SerializeField]
        private Box _automaticTurnTrigger;
        
        [Space]
        [SerializeField]
        private BoxCollider _invisibleWall;

        private readonly List<Uid> _automaticTurnTriggerEnterUid = new List<Uid>();
        private readonly List<Uid> _preTurnReactTriggerEnterHandledUid = new List<Uid>();
        private readonly List<Uid> _turnTriggerEnterHandledUid = new List<Uid>();
        private readonly List<Uid> _turnTriggerExitHandledUid = new List<Uid>();
        private int _automaticTurnUid;

        private ICommandBuffer _commandBuffer;
        private GameContext _game;
        private IPlayerBoxTracker _playerBoxTracker;
        private int _preTurnUid;
        private Uid _selfUid;
        private int _turnBoxUid;

        [Inject]
        private void Construct(ICommandBuffer commandBuffer, IPlayerBoxTracker playerBoxTracker, GameContext game)
        {
            // Более показать не могу
        }

        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }

        private void ActivateInvisibleWall(GameEntity entity)
        {
            // Более показать не могу
        }

        private void DeactivateInvisibleWall(GameEntity entity)
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

        private void OnTurnTriggerEntered()
        {
            // Более показать не могу
        }

        private void OnTurnTriggerExit()
        {
            // Более показать не могу
        }

        private void OnPreTurnReactTriggerEntered()
        {
            // Более показать не могу
        }

        private void OnAutomaticTurnTriggerEntered()
        {
            // Более показать не могу
        }

        private void OnPlayerRevived(GameEntity player)
        {
            // Более показать не могу
        }
    }
}