using Db.Sound;
using Ecs.Action.Commands.Sounds;
using Ecs.Utils;
using Game.Utils;
using Game.Utils.ColorsUtils;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;
using UnityEngine;

namespace Ecs.Game.Systems.Timer
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 770, nameof(EFeatures.Sound))]
    public class ChaserRoarTimerSystem : ForEachCommandUpdateSystem<StartChaserRoarTimerCommand>
    {
        private readonly IChaserSoundParameters _chaserSoundParameters;
        private readonly GameContext _game;

        public ChaserRoarTimerSystem(
            ICommandBuffer commandBuffer,
            IChaserSoundParameters chaserSoundParameters,
            GameContext game
        ) : base(commandBuffer)
        {
            _chaserSoundParameters = chaserSoundParameters;
            _game = game;
        }

        protected override void Execute(ref StartChaserRoarTimerCommand command)
        {
            // Более показать не могу
        }
    }
}