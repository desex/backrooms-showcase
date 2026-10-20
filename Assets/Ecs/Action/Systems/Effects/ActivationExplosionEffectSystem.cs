using Ecs.Action.Commands.Effects;
using Ecs.Utils;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems.Effects
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 804, nameof(EFeatures.Common))]
    public class ActivationExplosionEffectSystem : ForEachCommandUpdateSystem<ActivationExplosionEffectCommand>
    {
        private readonly GameContext _game;

        public ActivationExplosionEffectSystem(ICommandBuffer commandBuffer, GameContext game) : base(commandBuffer)
        {
            _game = game;
        }

        protected override void Execute(ref ActivationExplosionEffectCommand command)
        {
            var tutorialEntity = _game.TutorialEntity;
            var isTutorialActive = tutorialEntity.IsInTutorial;

            if (isTutorialActive)
                return;

            var player = _game.PlayerEntity;

            if (command.IsActive && (player.IsDash || player.IsBerserkMode || player.IsDamaged))
                return;

            player.IsExplosionEffect = command.IsActive;
        }
    }
}