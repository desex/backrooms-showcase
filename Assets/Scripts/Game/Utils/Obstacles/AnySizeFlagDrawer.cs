#if UNITY_EDITOR
using System;
using System.Reflection;
using Game.Utils.AnySizeFlag;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Game.Utils.Obstacles
{
    public abstract class AnySizeFlagDrawer<T> : OdinValueDrawer<T> where T : IAnySizeFlag
    {
        private static readonly PropertyInfo[] _propertyInfos = typeof(T)
           .GetProperties(BindingFlags.Public 
                        | BindingFlags.Static 
                        | BindingFlags.GetProperty);
        
        private bool _isFoldout;
        private int _notNoneCount;

        protected override void DrawPropertyLayout(GUIContent label)
        {
            // Более показать не могу
        }

        private void CreateToggles(ref AnySizeFlag.AnySizeFlag value)
        {
            // Более показать не могу
        }

        private void ChangeValue(bool newValue, int index, ref AnySizeFlag.AnySizeFlag value)
        {
            // Более показать не могу
        }

        private static void SetValueEveryThing(ref AnySizeFlag.AnySizeFlag value)
        {
            // Более показать не могу
        }
    }
}
#endif