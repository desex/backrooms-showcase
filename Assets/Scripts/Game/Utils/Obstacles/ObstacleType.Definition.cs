using System.Reflection;
using System.Text;
using Game.Utils.AnySizeFlag;
using UnityEngine;

namespace Game.Utils.Obstacles
{
    public partial struct ObstacleType 
#if UNITY_EDITOR
        : IAnySizeFlag
#endif
    {
        [SerializeField] private AnySizeFlag.AnySizeFlag _flagValue;
        
#if UNITY_EDITOR
        public AnySizeFlag.AnySizeFlag FlagValue => _flagValue;
        
        public ObstacleType(AnySizeFlag.AnySizeFlag flag)
        {
            _flagValue = flag;
        }
#endif

        public ObstacleType(long value)
        {
            _flagValue = value;
        }
        
        public static implicit operator ObstacleType(AnySizeFlag.AnySizeFlag flag)
        {
            return new ObstacleType
            {
                _flagValue = flag,
            };
        }
        
        public static implicit operator ObstacleType(long value)
        {
            return new ObstacleType
            {
                _flagValue = value,
            };
        }

        public static ObstacleType operator <<(ObstacleType a, int b)
        {
            return new ObstacleType
            {
                _flagValue = a._flagValue << b,
            };
        }

        public static ObstacleType operator &(ObstacleType a, ObstacleType b)
        {
            return a._flagValue & b._flagValue;
        }

        public static ObstacleType operator |(ObstacleType a, ObstacleType b)
        {
            return a._flagValue | b._flagValue;
        }

        public static ObstacleType operator ~(ObstacleType a)
        {
            return ~a._flagValue;
        }

        public static bool operator ==(ObstacleType a, ObstacleType b)
        {
            return a._flagValue == b._flagValue;
        }

        public static bool operator !=(ObstacleType a, ObstacleType b)
        {
            return a._flagValue != b._flagValue;
        }
        
        public bool Equals(ObstacleType other)
        {
            // Более показать не могу
            return default;
        }
        
        public override bool Equals(object obj)
        {
            // Более показать не могу
            return default;
        }
        
        public override int GetHashCode()
        {
            // Более показать не могу
            return default;
        }

        public override string ToString()
        {
            // Более показать не могу
            return default;
        }
    }
}