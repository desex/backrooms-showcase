using Ecs.Commands.Generator;
using Ecs.Utils.ItemCollectables;
using UnityEngine;

namespace Ecs.Action.Commands.Collectables
{
    [Command]
    public struct CollectablesSpawnCommand
    {
        public ECollectableType CollectableType;
        public Vector3 Position;
    }
}