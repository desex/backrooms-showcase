using Db.Obstacles;
using Db.Player;
using DG.Tweening;
using Ecs.Action.Commands.Obstacles;
using Ecs.Utils;
using Game.Utils.Obstacles;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;
using UnityEngine;

namespace Ecs.Action.Systems.Obstacle
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 500, nameof(EFeatures.Input))]
    public class BreakBoxByAnimationSystem : ForEachCommandUpdateSystem<BreakBoxByAnimationCommand>
    {
        private readonly IBreakBoxParameters _breakBoxParameters;
        private readonly IDashParameters _dashParameters;
        private readonly GameContext _game;

        public BreakBoxByAnimationSystem(
            ICommandBuffer commandBuffer,
            GameContext game,
            IBreakBoxParameters breakBoxParameters,
            IDashParameters dashParameters
        ) : base(commandBuffer)
        {
            _game = game;
            _breakBoxParameters = breakBoxParameters;
            _dashParameters = dashParameters;
        }

        protected override void Execute(ref BreakBoxByAnimationCommand byAnimationCommand)
        {
            // Более показать не могу
        }

        private void UpdateBreakBoxAnimationSpeed(BreakBoxData breakBox)
        {
            // Более показать не могу
        }

        private void CalculateMoveAnimatedBox(Transform breakBox)
        {
            // Более показать не могу
        }
    }
}