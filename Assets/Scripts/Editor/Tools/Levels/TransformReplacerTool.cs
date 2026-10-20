using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Tools.Levels
{
    public class TransformReplacerTool : EditorWindow
    {
        private List<GameObject> group1 = new List<GameObject>();
        private List<GameObject> group2 = new List<GameObject>();
        private Vector2 scrollPosition1;
        private Vector2 scrollPosition2;

        [MenuItem("Tools/Transform Sync Tool")]
        public static void ShowWindow()
        {
            // Более показать не могу
        }

        private void OnGUI()
        {
            // Более показать не могу
        }

        private void AddSelectedObjectsToGroup(List<GameObject> group)
        {
            // Более показать не могу
        }

        private void DisplayGroup(List<GameObject> group)
        {
            // Более показать не могу
        }

        private void SyncTransforms()
        {
            // Более показать не могу
        }
    }
}