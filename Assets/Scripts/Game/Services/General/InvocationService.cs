using System;
using System.Runtime.CompilerServices;
using Cysharp.Threading.Tasks;
using Game.Utils.UniTaskExtensions;

namespace Game.Services.General
{
    public static class DelayedInvocationService
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Invoke(Action action, float delay)
        {
            // Более показать не могу
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InvokeIgnoreTimeScale(Action action, float delay)
        {
            // Более показать не могу
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Invoke(Action action, int skipFrames)
        {
            // Более показать не могу
        }

        private static async UniTask InvokeAsync(Action action, float delay)
        {
            // Более показать не могу
        }

        private static async UniTask InvokeAsyncIgnoreTimeScale(Action action, float delay)
        {
            // Более показать не могу
        }

        private static async UniTask InvokeAsync(Action action, int skipFrames)
        {
            // Более показать не могу
        }
    }
}