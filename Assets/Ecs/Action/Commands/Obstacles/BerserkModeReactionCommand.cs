using System.Collections.Generic;
using Ecs.Commands.Generator;
using Game.Utils.BerserkReaction;
using UnityEngine;

namespace Ecs.Action.Commands.Obstacles
{
    [Command]
    public struct BerserkModeReactionCommand
    {
        public IReadOnlyList<BerserkReactionEffect> Effects;
        public IReadOnlyList<GameObject> ObjectsToDisable;
    }
}