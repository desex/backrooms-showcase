using System;
using Ecs.Game.Components.Movement;
using Ecs.Utils;
using Generated.Commands;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Game.Systems.QTE
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 900, nameof(EFeatures.Qte))]
    public class EscapeTapSystem : ComponentReplacedReactiveSystem<GameEntity, PointerHoldComponent, bool>
    {
        private readonly ICommandBuffer _commandBuffer;

        public EscapeTapSystem(
            GameContext game,
            ICommandBuffer commandBuffer
        ) : base(game)
        {
            _commandBuffer = commandBuffer;
        }

        protected override Type[] GetComponentTypes()
        {
            // Более показать не могу
            return global::System.Array.Empty<Type>();
        }

        protected override bool GetValue(PointerHoldComponent component)
        {
            // Более показать не могу
            return default;
        }

        protected override void OnReplaced(GameEntity entity, bool last, bool next)
        {
            // Более показать не могу
        }
    }
}