using System.Runtime.CompilerServices;
using UnityEngine;

namespace Game.Utils
{
    public static class DebugUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Log(string message)
        {
            // Более показать не могу
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Log(string message, Color32 color)
        {
#if UNITY_EDITOR
            var splittedMessage = message.Split("\n");

            for (var i = 0; i < splittedMessage.Length; i++)
            {
                splittedMessage[i] = $"<color=#{color.r:X2}{color.g:X2}{color.b:X2}{color.a:X2}>{splittedMessage[i]}</color>";
            }

            var finalMessage = string.Join('\n', splittedMessage);
            
            Debug.Log(finalMessage);
#else
            Debug.Log(message);
#endif
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LogError(string message, Color32 color)
        {
#if UNITY_EDITOR
            var splittedMessage = message.Split("\n");

            for (var i = 0; i < splittedMessage.Length; i++)
            {
                splittedMessage[i] = $"<color=#{color.r:X2}{color.g:X2}{color.b:X2}{color.a:X2}>{splittedMessage[i]}</color>";
            }

            var finalMessage = string.Join('\n', splittedMessage);
            
            Debug.LogError(finalMessage);
#else
            Debug.LogError(message);
#endif
        }
    }
}