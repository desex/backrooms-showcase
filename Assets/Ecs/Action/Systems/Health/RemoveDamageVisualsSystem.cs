using Db.Effects;
using Ecs.Action.Commands.Health;
using Ecs.Utils;
using Game.UI.Effects;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems.Health
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 969, nameof(EFeatures.Move))]
    public class RemoveDamageVisualsSystem : ForEachCommandUpdateSystem<RemoveDamageVisualsCommand>
    {
        private readonly GameContext _game;
        private readonly IUiEffectController _uiEffectController;

        public RemoveDamageVisualsSystem(
            ICommandBuffer commandBuffer,
            GameContext game,
            IUiEffectController uiEffectController
        ) : base(commandBuffer)
        {
            _game = game;
            _uiEffectController = uiEffectController;
        }

        protected override void Execute(ref RemoveDamageVisualsCommand command)
        {
            var character = _game.GetEntityWithUid(command.CharacterUid);

            if (character.IsDead)
                return;

            if (character.IsPlayer)
                _uiEffectController.StopEffects(EUiEffectType.BloodEffect);

            character.IsDamagedVisually = false;
        }
    }
}