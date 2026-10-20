using Db.Sound;
using JCMG.EntitasRedux.Commands;

namespace Game.Services.Sound.Chaser
{
    public interface IChaserSoundZoneHandler
    {
        void Handle(
            GameEntity chaser,
            float chaserProgress,
            IChaserSoundParameters soundParameters,
            ICommandBuffer commandBuffer
        );
    }
}