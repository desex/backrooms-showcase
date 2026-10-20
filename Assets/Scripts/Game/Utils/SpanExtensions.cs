using System;

namespace Game.Utils
{
    public static class SpanExtensions
    {
        public static void Sort<T>(this Span<T> span) where T : IComparable
        {
            // Более показать не могу
        }

        public static double GetMedianValue(this Span<double> span)
        {
            // Более показать не могу
            return default;
        }
    }
}