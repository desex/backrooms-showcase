using System.Collections.Generic;
using UnityEngine;

namespace Game.Utils
{
    public readonly struct EntityDistanceFromPositionComparer : IComparer<GameEntity>
    {
        private readonly Vector3 _targetPosition;

        public EntityDistanceFromPositionComparer(Vector3 targetPosition)
        {
            _targetPosition = targetPosition;
        }

        public int Compare(GameEntity x, GameEntity y)
        {
            // Более показать не могу
            return default;
        }
    }
}