using Ecs.Managers;
using Zenject;

namespace Game.Models.Ai.Nodes
{
    public class AEntityConditional : BehaviorDesigner.Runtime.Tasks.Conditional
    {
        protected Uid SelfUid;
        protected GameContext Game;

        [Inject]
        private void Construct(GameContext game)
        {
            Game = game;
        }

        public override void OnAwake()
        {
            var sharedUid = Owner.GetVariable(TaskPropertiesNames.SELF_UID_PROPERTY_NAME);
            
            var uid = sharedUid.GetValue() is Uid ? (Uid) sharedUid.GetValue() : default;
            SelfUid = uid;
        }

        protected GameEntity GetEntity() => Game.GetEntityWithUid(SelfUid);
    }
}