using Db.Player;
using DG.Tweening;
using Ecs.Action.Commands.Obstacles;
using Ecs.Utils;
using Game.Services.Effects;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems.Obstacle
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 755, nameof(EFeatures.Move))]
    public class HandleWaterEnterSystem : ForEachCommandUpdateSystem<HandleWaterEnterCommand>
    {
        private readonly GameContext _game;
        private readonly IPlayerWaterInHoleParametersBase _playerWaterInHoleParameters;
        private readonly IUnderWaterRendererService _underWaterRendererService;

        public HandleWaterEnterSystem(
            ICommandBuffer commandBuffer,
            GameContext game,
            IUnderWaterRendererService underWaterRendererService,
            IPlayerWaterInHoleParametersBase playerWaterInHoleParameters
        ) : base(commandBuffer)
        {
            _game = game;
            _underWaterRendererService = underWaterRendererService;
            _playerWaterInHoleParameters = playerWaterInHoleParameters;
        }

        protected override void Execute(ref HandleWaterEnterCommand command)
        {
            // Более показать не могу
        }

        private void StartGravityTween(GameEntity entity)
        {
            if (entity.HasWaterGravityTween)
                entity.WaterGravityTween.Value?.Kill();

            var fromSpeed = entity.VerticalSpeed.Value;

            var sequence = DOTween.Sequence();

            sequence.Append(
                ApplyGravityForceSmoothly(
                    fromSpeed,
                    _playerWaterInHoleParameters.FastWaterEnterVerticalSpeed,
                    _playerWaterInHoleParameters.FastWaterEnterDuration,
                    _playerWaterInHoleParameters.FastWaterEnterEase
                )
            );

            sequence.Append(
                ApplyGravityForceSmoothly(
                    _playerWaterInHoleParameters.FastWaterEnterVerticalSpeed,
                    _playerWaterInHoleParameters.StayInWaterVerticalSpeed,
                    _playerWaterInHoleParameters.SlowdownAfterEnterDuration,
                    _playerWaterInHoleParameters.SlowdownAfterEnterEase
                )
            );

            sequence.OnUpdate(() =>
                {
                    if (entity.IsInWater && !entity.IsDestroyed)
                        return;

                    sequence?.Kill();

                    if (entity.HasWaterGravityTween)
                        entity.RemoveWaterGravityTween();
                }
            );

            entity.ReplaceWaterGravityTween(sequence);

            return;

            Tween ApplyGravityForceSmoothly(float fromGravity, float targetGravity, float duration, Ease ease)
            {
                return DOVirtual.Float(fromGravity, targetGravity, duration, entity.ReplaceInWaterVerticalSpeed)
                    .SetEase(ease);
            }
        }
    }
}