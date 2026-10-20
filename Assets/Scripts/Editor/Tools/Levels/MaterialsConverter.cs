using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Tools.Levels
{
    public class MaterialsConverter : EditorWindow
    {
        [FormerlySerializedAs("materialMapping")]
        public MaterialsMapper materialMapper;
        public GameObject[] prefabs;

        [MenuItem("Tools/Material Replacer")]
        public static void ShowWindow()
        {
            // Более показать не могу
        }

        private void OnGUI()
        {
            // Более показать не могу
        }

        private void ReplaceMaterials()
        {
            // Более показать не могу
        }
    }
}