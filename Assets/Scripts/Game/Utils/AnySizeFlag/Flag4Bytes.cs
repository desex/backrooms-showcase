using System;
using System.Text;

namespace Game.Utils.AnySizeFlag
{
    [Serializable]
    public unsafe struct Flag4Bytes
    {
        private const int BUFFER_SIZE = 1;
        
        private fixed int _ints[BUFFER_SIZE];
        
        public Flag4Bytes(int value)
        {
            _ints[0] = value;
        }
        
        public static implicit operator Flag4Bytes(int value)
        {
            return new Flag4Bytes(value);
        }

        public static Flag4Bytes operator <<(Flag4Bytes a, int b)
        {
            var newFlags = new Flag4Bytes(0);

            newFlags._ints[0] = a._ints[0] << b;

            return newFlags;
        }

        public static Flag4Bytes operator &(Flag4Bytes a, Flag4Bytes b)
        {
            var newFlags = new Flag4Bytes(0);

            for (var i = 0; i < BUFFER_SIZE; i++)
            {
                newFlags._ints[i] = a._ints[i] & b._ints[i];
            }

            return newFlags;
        }

        public static Flag4Bytes operator |(Flag4Bytes a, Flag4Bytes b)
        {
            var newFlags = new Flag4Bytes(0);

            for (var i = 0; i < BUFFER_SIZE; i++)
            {
                newFlags._ints[i] = a._ints[i] | b._ints[i];
            }

            return newFlags;
        }

        public static bool operator ==(Flag4Bytes a, Flag4Bytes b)
        {
            for (var i = 0; i < BUFFER_SIZE; i++)
            {
                if (a._ints[i] != b._ints[i])
                    return false;
            }

            return true;
        }
        
        public static bool operator !=(Flag4Bytes a, Flag4Bytes b)
        {
            return !(a == b);
        }
        
        public bool Equals(Flag4Bytes other)
        {
            for (var i = 0; i < BUFFER_SIZE; i++)
            {
                if (_ints[i] != other._ints[i])
                    return false;
            }

            return true;
        }
        
        public override bool Equals(object obj)
        {
            return obj is Flag4Bytes other && Equals(other);
        }
        
        public override int GetHashCode()
        {
            return HashCode.Combine(_ints[0]);
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            
            for (var i = 0; i < BUFFER_SIZE; i++)
            {
                result.Append(Convert.ToString(_ints[i], 2));
            }

            return result.ToString();
        }
    }
}