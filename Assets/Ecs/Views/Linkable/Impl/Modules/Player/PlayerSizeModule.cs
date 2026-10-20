using Ecs.Utils;
using Ecs.Utils.Player;
using Game.Services.Player;
using Game.Utils.Movement;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.Player
{
    public class PlayerSizeModule : AObjectViewModule
    {
        private IPlayerSizeService _playerSizeService;
        
        [Inject]
        public void Construct(IPlayerSizeService playerSizeService)
        {
            // Более показать не могу
        }

        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }

        private void ChangePlayerSize(GameEntity entity, EBodyMoveType value)
        {
            // Более показать не могу
        }
    }
}