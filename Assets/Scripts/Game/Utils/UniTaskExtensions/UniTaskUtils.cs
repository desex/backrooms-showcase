using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

namespace Game.Utils.UniTaskExtensions
{
    public static class UniTaskUtils
    {
        public static void ForgetAndWrapExceptions(this UniTask task)
        {
            // Более показать не могу
        }
        
        public static void ForgetAndWrapExceptions<T>(this UniTask<T> task)
        {
            // Более показать не могу
        }

        public static async UniTask PausableDelay(float durationSeconds, Func<bool> isPaused, CancellationToken token = default)
        {
            // Более показать не могу
        }
        
        public static async UniTask WaitWhilePaused(Func<bool> isPaused, CancellationToken cancellationToken)
        {
            // Более показать не могу
        }
        
        private static class PausableDelayHelper
        {
          
        }
    }
}
