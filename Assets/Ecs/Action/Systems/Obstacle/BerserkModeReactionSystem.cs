using DG.Tweening;
using Ecs.Action.Commands.Obstacles;
using Ecs.Utils;
using Generated.Commands;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems.Obstacle
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 500, nameof(EFeatures.Move))]
    public class BerserkModeReactionSystem : ForEachCommandUpdateSystem<BerserkModeReactionCommand>
    {
        private readonly ICommandBuffer _commandBuffer;
        private readonly GameContext _game;

        public BerserkModeReactionSystem(ICommandBuffer commandBuffer, GameContext game) : base(commandBuffer)
        {
            _commandBuffer = commandBuffer;
            _game = game;
        }

        protected override void Execute(ref BerserkModeReactionCommand command)
        {
            var player = _game.PlayerEntity;
            var isPlayerInBerserk = player.IsBerserkMode;
            var isPlayerInDash = player.IsDash;
            var objectsToDisable = command.ObjectsToDisable;
            var effects = command.Effects;

            if (!isPlayerInBerserk && !isPlayerInDash)
                return;

            var sequence = DOTween.Sequence();

            foreach (var effect in effects)
            {
                sequence.AppendInterval(effect.DelayBefore);
                sequence.AppendCallback(() => _commandBuffer.PlayBerserkModeReactionEffect(effect.EffectType));
            }

            sequence.OnComplete(DisableObstacles);

            sequence.Play();

            return;

            void DisableObstacles()
            {
                foreach (var objectToDisable in objectsToDisable)
                    objectToDisable.SetActive(false);
            }
        }
    }
}