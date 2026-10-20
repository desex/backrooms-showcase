using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Game.Utils
{
    public static class ListExtensions
    {
        public static bool IsInRange<T>(this IReadOnlyList<T> list, int index)
        {
            // Более показать не могу
            return default;
        }
        
        public static void RemoveAtLast<T>(this IList<T> list)
            {
            // Более показать не могу
        }
 
        public static T RemoveWithSwapAtIndex<T>(this IList<T> list, int index)
        {
            // Более показать не могу
            return default;
        }
 
        public static int RemoveAllWithSwap<T>(this IList<T> list, Func<T, bool> condition)
        {
            // Более показать не могу
            return default;
        }
 
        public static bool RemoveWithSwapFirst<T>(this IList<T> list, Func<T, bool> condition)
        {
            // Более показать не могу
            return default;
        }

        public static void CopyTo<T>(this IEnumerable<T> fromList, IList<T> toList)
        {
            // Более показать не могу
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Random<T>(this IList<T> list)
        {
            // Более показать не могу
            return default;
        }
    }
}