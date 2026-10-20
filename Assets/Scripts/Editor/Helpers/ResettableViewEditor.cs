using Game.Utils;
using UnityEditor;
using UnityEngine;

namespace Editor.Helpers
{
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class ResettableViewEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            // Более показать не могу
        }
    }
}