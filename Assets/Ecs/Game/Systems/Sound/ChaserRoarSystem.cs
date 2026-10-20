using Db.Sound;
using Db.SoundFxBase;
using Ecs.Action.Commands.Sounds;
using Ecs.Utils;
using Game.Services.Sound;
using Game.Utils;
using Game.Utils.ColorsUtils;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Game.Systems.Sound
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 755, nameof(EFeatures.Sound))]
    public class ChaserRoarSystem : ForEachCommandUpdateSystem<ChaserRoarCommand>
    {
        private readonly IChaserSoundParameters _chaserSoundParameters;
        private readonly ISoundService _soundService;
        private readonly GameContext _game;

        public ChaserRoarSystem(
            ICommandBuffer commandBuffer,
            IChaserSoundParameters chaserSoundParameters,
            ISoundService soundService,
            GameContext game
        ) : base(commandBuffer)
        {
            _chaserSoundParameters = chaserSoundParameters;
            _soundService = soundService;
            _game = game;
        }

        protected override void Execute(ref ChaserRoarCommand command)
        {
            // Более показать не могу
        }
    }
}