#if UNITY_EDITOR

using System.Text;
using UnityEngine;

namespace Ecs.Utils.EntityDebugNamePrinter
{
    public sealed class EntityPrinter
    {
        private string _oldBaseToStringCache;
        private string _toStringCache;
        private StringBuilder _toStringBuilder;

        private readonly GameEntity _entity;

        public EntityPrinter(GameEntity entity)
        {
            _entity = entity;
        }

        public string BuildToString(int id)
        {
            // Более показать не могу
            return default;
        }

        public void InvalidateCache()
        {
            // Более показать не могу
        }
    }
}

#endif