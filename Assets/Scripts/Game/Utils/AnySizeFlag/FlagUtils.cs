using System;

namespace Game.Utils.AnySizeFlag
{
    public static class FlagUtils
    {
        public static void IntToSpan(int value, Span<bool> span32Bit)
        {
            for (var i = 0; i < 32; i++)
            {
                span32Bit[i] = (value & (1 << i)) != 0;
            }
        }

        public static int SpanToInt(ReadOnlySpan<bool> span32Bit)
        {
            var value = 0;

            for (var i = 0; i < 32; i++)
            {
                value = span32Bit[i] ? value | (1 << i) : value;
            }

            return value;
        }
    }
}