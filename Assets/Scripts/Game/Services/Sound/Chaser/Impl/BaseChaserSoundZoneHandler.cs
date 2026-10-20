using Db.Sound;
using Generated.Commands;
using JCMG.EntitasRedux.Commands;

namespace Game.Services.Sound.Chaser.Impl
{
    public class BaseChaserSoundZoneHandler : IChaserSoundZoneHandler
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