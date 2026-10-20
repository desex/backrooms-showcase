using Db.ChasePlayer;
using DG.Tweening;
using Ecs.Action.Commands.Sounds;
using Ecs.Utils;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;
using UnityEngine;

namespace Ecs.Action.Systems.Sounds
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 900, nameof(EFeatures.Sound))]
    public class ChangeMonsterSoundDistanceSystem : ForEachCommandUpdateSystem<ChangeMonsterSoundDistanceCommand>
    {
        private readonly IChasePlayerParameters _chasePlayerParameters;

        private Tween _tween;

        public ChangeMonsterSoundDistanceSystem(
            IChasePlayerParameters chasePlayerParameters,
            ICommandBuffer commandBuffer
        ) : base(commandBuffer)
        {
            _chasePlayerParameters = chasePlayerParameters;
        }

        protected override void Execute(ref ChangeMonsterSoundDistanceCommand command)
        {
            var soundPointIndex = (int)command.MovementState - 1;

            if (soundPointIndex < 0 || soundPointIndex > command.SoundPoints.Count)
            {
                Debug.LogError(
                    $"[{nameof(ChangeMonsterSoundDistanceSystem)}]: Wrong movement state {command.MovementState}"
                );

                return;
            }

            var targetPoint = command.SoundPoints[soundPointIndex];
            var soundTransform = command.SoundTransform;
            var duration = _chasePlayerParameters.TimeToInterpolateProgressBar;
            var ease = _chasePlayerParameters.MoveEase;

            _tween?.Kill();
            _tween = soundTransform.DOLocalMove(targetPoint, duration).SetEase(ease);
        }
    }
}