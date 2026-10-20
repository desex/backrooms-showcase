using Core.Utils.TriggerCollision;
using Ecs.Utils;
using Ecs.Views.Linkable.Impl.Modules.Obstacles.Jobs;
using UnityEngine;
using Zenject;

namespace Game.Utils.Chunks
{
    public class ExtraGravityApplierModule : AObjectViewModule
    {
        [SerializeField]
        private Box _extraGravityTrigger;

        private GameContext _game;
        private IPlayerBoxTracker _playerBoxTracker;
        private int _triggerUid;

        [Inject]
        private void Construct(GameContext game, IPlayerBoxTracker playerBoxTracker)
        {
            _game = game;
            _playerBoxTracker = playerBoxTracker;
        }

        public override void Activate()
        {
            _triggerUid = TriggerUidGenerator.Next();

            var extraGravityBoxActionData = new BoxActionData
            {
                Box = _extraGravityTrigger,
                Name = name,
                GlobalPosition = transform.position,
                Uid = _triggerUid,
                EnterAction = ActivateExtraGravity,
                ExitAction = DeactivateExtraGravity,
            };

            _playerBoxTracker.AddBox(extraGravityBoxActionData);
        }

        public override void Deactivate()
        {
            _playerBoxTracker.RemoveBox(_triggerUid);
        }

        private void ActivateExtraGravity()
        {
            var player = _game.PlayerEntity;
            player.IsUseExtraGravity = true;
        }

        private void DeactivateExtraGravity()
        {
            var player = _game.PlayerEntity;
            player.IsUseExtraGravity = false;
        }
    }
}