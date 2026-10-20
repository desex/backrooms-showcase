using Ecs.Commands.Generator;
using Ecs.Managers;

namespace Ecs.Action.Commands.Collectables
{
    [Command]
    public struct CollectablesSelectionCommand
    {
        public Uid CollectableId;
    }
}