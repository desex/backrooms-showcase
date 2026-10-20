using System.Collections.Generic;
using System.Reflection;
using Ecs.Utils;
using Ecs.Views.Linkable.Impl;
using Ecs.Views.Linkable.Impl.Modules.Revive;
using Ecs.Views.Linkable.Impl.Obstacles;
using Game.Utils;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Editor.Tools.PrefabChecker
{
    public class PrefabChecker : EditorWindow
    {
        private const string PrefabRoot = "Assets/Prefabs";
        private bool _isRunning;
        private bool _cancelRequested;
        private string[] _prefabGuids;
        private int _index, _total, _errors, _warnings;

        [MenuItem("Tools/Prefab Checker")]
        private static void ShowWindow() {
            // Более показать не могу
        }

        private void OnGUI()
        {
            // Более показать не могу
        }

        private void StartValidation()
        {
            // Более показать не могу
        }

        private void ValidateStep()
        {
            // Более показать не могу
        }

        private void FinishValidation(bool cancel)
        {
            // Более показать не могу
        }

        private static void ValidateOpenedPrefab()
        {
            // Более показать не могу
        }

        private static void ClearConsole()
        {
            // Более показать не могу
        }

        private static List<AObjectViewModule> GetModules(ObjectView view)
        {
            // Более показать не могу
            return default;
        }

        private static Object Ctx(
            Object inst,
            Object prefabAsset,
            bool useInstanceCtx
        )
        {
            // Более показать не могу
            return default;
        }

        private static void LogErrors(
            string msg,
            Object inst,
            Object prefabAsset,
            ref int errors,
            bool useInstanceCtx
        )
        {
            // Более показать не могу
        }

        private static void LogWarnings(
            string msg,
            Object inst,
            Object prefabAsset,
            ref int errors,
            bool useInstanceCtx
        )
        {
            // Более показать не могу
        }

        private static void CheckDuplicates<T>(
            IEnumerable<T> items,
            string owner,
            string prop,
            ref int errors,
            Object prefabAsset,
            bool useInstanceCtx
        )
        {
            // Более показать не могу
        }

        private static void ValidateObjectViewModulesCommon(
            Transform root,
            string tag,
            Object prefabAsset,
            ref int errors,
            bool useInstanceContext
        )
        {
            // Более показать не могу
        }

        private static void ValidateObstaclesCommon(
            Transform root,
            string tag,
            Object prefabAsset,
            bool requireParent,
            ref int errors,
            bool useInstanceContext
        )
        {
            // Более показать не могу
        }

        private static void ValidateReviveObjectHiderModules(
            Transform root,
            string tag,
            Object prefabAsset,
            ref int errors,
            bool useInstanceContext)
        {
            // Более показать не могу
        }
    }
}