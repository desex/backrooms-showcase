using System;
using System.Text;

namespace Game.Utils.AnySizeFlag
{ 
    [Serializable]
    public unsafe struct AnySizeFlag
    {
        private const int BUFFER_SIZE = 2;
        
        public fixed int Ints[BUFFER_SIZE];

        public static AnySizeFlag None => 0;
        
        public AnySizeFlag(long value)
        {
            var valuePointer = (int*)&value;
            Ints[0] = *valuePointer;
            Ints[1] = *(valuePointer + 1);
        }

        public AnySizeFlag(ReadOnlySpan<int> integers)
        {
            for (var i = 0; i < BUFFER_SIZE && i < integers.Length; i++)
            {
                Ints[i] = integers[i];
            }
        }
        
        public static implicit operator AnySizeFlag(long value)
        {
            return new AnySizeFlag(value);
        }

        public static AnySizeFlag operator <<(AnySizeFlag value, int offset)
        {
            Span<bool> bitSpan = stackalloc bool[32 * BUFFER_SIZE];

            for (var i = 0; i < BUFFER_SIZE; i++)
            {
                FlagUtils.IntToSpan(value.Ints[i], bitSpan.Slice(i * 32, 32));
            }

            for (var i = bitSpan.Length - 1; i >= 0; i--)
            {
                if (i - offset >= 0)
                    bitSpan[i] = bitSpan[i - offset];
                else
                    bitSpan[i] = false;
            }

            var newFlags = new AnySizeFlag(0);

            for (var i = 0; i < BUFFER_SIZE; i++)
            {
                newFlags.Ints[i] = FlagUtils.SpanToInt(bitSpan.Slice(i * 32, 32));
            }

            return newFlags;
        }

        public static AnySizeFlag operator &(AnySizeFlag a, AnySizeFlag b)
        {
            var newFlags = new AnySizeFlag(0);

            for (var i = 0; i < BUFFER_SIZE; i++)
            {
                newFlags.Ints[i] = a.Ints[i] & b.Ints[i];
            }

            return newFlags;
        }

        public static AnySizeFlag operator |(AnySizeFlag a, AnySizeFlag b)
        {
            var newFlags = new AnySizeFlag(0);

            for (var i = 0; i < BUFFER_SIZE; i++)
            {
                newFlags.Ints[i] = a.Ints[i] | b.Ints[i];
            }

            return newFlags;
        }

        public static AnySizeFlag operator ~(AnySizeFlag a)
        {
            var newFlags = new AnySizeFlag(0);
            
            for (var i = 0; i < BUFFER_SIZE; i++)
            {
                newFlags.Ints[i] = ~a.Ints[i];
            }
            
            return newFlags;
        }

        public static bool operator ==(AnySizeFlag a, AnySizeFlag b)
        {
            for (var i = 0; i < BUFFER_SIZE; i++)
            {
                if (a.Ints[i] != b.Ints[i])
                    return false;
            }

            return true;
        }
        
        public static bool operator !=(AnySizeFlag a, AnySizeFlag b)
        {
            return !(a == b);
        }
        
        public bool Equals(AnySizeFlag other)
        {
            for (var i = 0; i < BUFFER_SIZE; i++)
            {
                if (Ints[i] != other.Ints[i])
                    return false;
            }

            return true;
        }
        
        public override bool Equals(object obj)
        {
            return obj is AnySizeFlag other && Equals(other);
        }
        
        public override int GetHashCode()
        {
            var result = 0;
            
            for (var i = 0; i < BUFFER_SIZE; i++)
            {
                result = HashCode.Combine(result, Ints[i]);
            }

            return result;
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            
            for (var i = BUFFER_SIZE - 1; i >= 0; i--)
            {
                result.Append(Convert.ToString(Ints[i], 2).PadLeft(32, '0'));
            }

            return result.ToString();
        }
    }
}