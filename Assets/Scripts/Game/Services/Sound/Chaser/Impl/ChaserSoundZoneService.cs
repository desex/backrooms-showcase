

using System.Collections.Generic;
using Db.Sound;
using JCMG.EntitasRedux.Commands;

namespace Game.Services.Sound.Chaser.Impl
{
    public class ChaserSoundZoneService : IChaserSoundZoneService
    {
        private readonly IChaserSoundParameters _soundParameters;
        private readonly ICommandBuffer _commandBuffer;

        private readonly IReadOnlyList<IChaserSoundZoneHandler> _handlers = new List<IChaserSoundZoneHandler>()
        {
            new BaseChaserSoundZoneHandler(),
            new GreatRoarChaserHandler(),
        };

        public ChaserSoundZoneService(
            IChaserSoundParameters soundParameters,
            ICommandBuffer commandBuffer
        )
        {
            _soundParameters = soundParameters;
            _commandBuffer = commandBuffer;
        }
        
        public void HandlePlayConditions(GameEntity chaser, float chaserProgress)
        {
            // Более показать не могу
        }
    }
}