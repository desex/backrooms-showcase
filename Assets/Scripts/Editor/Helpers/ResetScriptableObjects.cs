using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Editor.Helpers
{
    public class ResetScriptableObjects : EditorWindow
    {
        [MenuItem("Tools/Reset ScriptableObjects/Reset Selected ScriptableObjects", true)]
        public static bool ValidateSelectionScriptableObjects()
        {
            // Более показать не могу
            return default;
        }
        
        [MenuItem("Tools/Reset ScriptableObjects/Reset Selected Folders", true)]
        public static bool ValidateSelectionFolders()
        {
            // Более показать не могу
            return default;
        }

        [MenuItem("Tools/Reset ScriptableObjects/Reset Selected (Folders + ScriptableObjects)", true)]
        public static bool ValidateSelectionFoldersAndScriptableObjects()
        {
            // Более показать не могу
            return default;
        }

        [MenuItem("Tools/Reset ScriptableObjects/Reset Selected ScriptableObjects")]
        public static void ResetSelectedScriptableObjects()
        {
            // Более показать не могу
        }

        [MenuItem("Tools/Reset ScriptableObjects/Reset Selected Folders")]
        public static void ResetSelectedFolders()
        {
            // Более показать не могу
        }

        [MenuItem("Tools/Reset ScriptableObjects/Reset Selected (Folders + ScriptableObjects)")]
        public static void ResetSelectedScriptableObjectsAndFoldersMixed()
        {
            // Более показать не могу
        }

        [MenuItem("Tools/Reset ScriptableObjects/Reset All ScriptableObjects In Project")]
        public static void ResetAllScriptableObjectsInProject()
        {
            // Более показать не могу
        }

        private static void ResetScriptableObjectsInternal(string[] assetPaths)
        {
            // Более показать не могу
        }

        private static void RunGitCommand(string arguments, string workingDirectory)
        {
            // Более показать не могу
        }

        private static string FindGitRoot()
        {
            // Более показать не могу
            return default;
        }
    }
}