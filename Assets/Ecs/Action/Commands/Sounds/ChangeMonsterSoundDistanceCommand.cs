using System.Collections.Generic;
using Ecs.Commands.Generator;
using Ecs.Utils;
using UnityEngine;

namespace Ecs.Action.Commands.Sounds
{
    [Command]
    public struct ChangeMonsterSoundDistanceCommand
    {
        public Transform SoundTransform;
        public List<Vector3> SoundPoints;
        public EMovementState MovementState;
    }
}