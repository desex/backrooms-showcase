using Ecs.Action.Commands.Movement.ObstaclePass;
using Ecs.Utils;
using Ecs.Utils.ObstaclePass;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;
using UnityEngine;

namespace Ecs.Action.Systems.Movement.ObstaclePass
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 250, nameof(EFeatures.Move))]
    public class SetReadyToPassObstacleSystem : ForEachCommandUpdateSystem<SetReadyToPassObstacleCommand>
    {
        private readonly GameContext _game;

        public SetReadyToPassObstacleSystem(ICommandBuffer commandBuffer, GameContext game) : base(commandBuffer)
        {
            _game = game;
        }

        protected override void Execute(ref SetReadyToPassObstacleCommand obstacleCommand)
        {
            var character = _game.GetEntityWithUid(obstacleCommand.CharacterUid);

            if (character.HasReadyToObstaclePass)
                return;

            var obstaclePassData = obstacleCommand.ObstaclePassData;

            if (obstaclePassData.MovePoints == null)
            {
                Debug.LogError($"[{nameof(SetReadyToPassObstacleSystem)}]: There is no move points in command!");

                return;
            }

            character.IsCanDash = true;

            character.ReplaceReadyToObstaclePass(
                new ReadyToObstaclePassData(
                    obstaclePassData.Uid,
                    obstaclePassData.ObstaclePassType,
                    obstaclePassData.IsDashEnabled,
                    obstaclePassData.ObstacleTypeForAcceleration
                )
            );

            character.ReplaceDashWhenInObstacle(
                new DashData(
                    obstaclePassData.DashDuration,
                    character.Position.Value,
                    obstaclePassData.DashEndPosition,
                    obstaclePassData.DashEase,
                    obstaclePassData.DashComboMoveMultiplier,
                    obstaclePassData.CenterStartReadyTriggerPosition
                )
            );

            character.ReplacePassObstacleMovePoints(
                new PassObstacleData(
                    obstaclePassData.ObstaclePassComboMoveMultiplier,
                    obstaclePassData.MovePoints,
                    obstaclePassData.CenterStartReadyTriggerPosition
                )
            );

            character.ReplaceChosenPassObstacleType(EPassObstacleType.NoReact);
        }
    }
}