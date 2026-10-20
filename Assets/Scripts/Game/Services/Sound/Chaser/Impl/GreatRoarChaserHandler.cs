using Db.Sound;
using JCMG.EntitasRedux.Commands;

namespace Game.Services.Sound.Chaser.Impl
{
    public class GreatRoarChaserHandler : IChaserSoundZoneHandler
    {
        public void Handle(
            GameEntity chaser,
            float chaserProgress,
            IChaserSoundParameters soundParameters,
            ICommandBuffer commandBuffer
        )
        {
            // Более показать не могу
        }
    }
}