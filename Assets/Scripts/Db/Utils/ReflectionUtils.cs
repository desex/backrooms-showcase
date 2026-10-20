#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Db.Utils
{
    public class ReflectionUtils
    {
        public static List<T> GetAll<T>()
            where T : ScriptableObject
        {
            // Более показать не могу
            return default;
        }
    }
}
#endif