using Ecs.Managers;
using Game.Utils.Rebound;
using UnityEngine;

namespace Game.Utils
{
    public readonly struct HitObstacleData
    {
        public readonly Vector3 MoveDirection;
        public readonly int ObjectHash;
        public readonly ReboundSetup ReboundSetup;
        public readonly Uid CharacterUid;

        public HitObstacleData(
            Vector3 moveDirection,
            int objectHash,
            ReboundSetup reboundSetup,
            Uid characterUid
        )
        {
            MoveDirection = moveDirection;
            ObjectHash = objectHash;
            ReboundSetup = reboundSetup;
            CharacterUid = characterUid;
        }
    }
}