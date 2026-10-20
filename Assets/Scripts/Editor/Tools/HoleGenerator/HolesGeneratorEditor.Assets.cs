using System.IO;
using Db.HoleGenerator.Impl;
using UnityEditor;
using UnityEngine;
using Directory = UnityEngine.Windows.Directory;

namespace Editor.Tools.HoleGenerator
{
    public partial class HoleGeneratorEditor
    {
        private void SaveHolePrefab()
        {
            var hole = GameObject.Find($"{_holeName}");
            
            Debug.Assert(hole != null, "Save Hole not found");
        
            var folderPath = EditorUtility.SaveFolderPanel("Выберите папку для сохранения", _defaultPathSave, "");
            
            Debug.Assert(!string.IsNullOrEmpty(folderPath), "Сохранение отменен");
        
            var relativePath = folderPath;
            if (folderPath.StartsWith(Application.dataPath))
            {
                relativePath = "Assets" + folderPath.Substring(Application.dataPath.Length);
            }
        
            if (!Directory.Exists(relativePath))
            {
                Directory.CreateDirectory(relativePath);
            }
        
            var prefabPath = Path.Combine(relativePath, $"{_holeName}.prefab");
            prefabPath = AssetDatabase.GenerateUniqueAssetPath(prefabPath);
        
            PrefabUtility.SaveAsPrefabAsset(hole, prefabPath, out var success);
        
            if (success)
            {
                AssetDatabase.Refresh();
                Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(prefabPath);
                DestroyImmediate(hole);
            }
            
            Debug.Assert(success, "Ошибка при сохранении префаба!");
        }
    
        private void FindHolesParameters()
        {
            var guids = AssetDatabase.FindAssets($"t:{nameof(HoleConstructorParameters)}");
        
            Debug.Assert(guids.Length != 0, $"Не найдено SO типа {nameof(HoleConstructorParameters)}!");
        
            var path = AssetDatabase.GUIDToAssetPath(guids[0]);
            _parameters = AssetDatabase.LoadAssetAtPath<HoleConstructorParameters>(path);
            
            Debug.Assert(_parameters != null, $"Не удалось загрузить SO типа {nameof(HoleConstructorParameters)}!");
        }
        
        private void FindGeneratorPrefabsSo()
        {
            var guids = AssetDatabase.FindAssets($"t:{nameof(HoleGeneratorPrefabsParameters)}");
        
            if (guids.Length == 0)
            {
                Debug.LogError($"Не найдено SO типа {nameof(HoleGeneratorPrefabsParameters)}!");
            }
        
            var path = AssetDatabase.GUIDToAssetPath(guids[0]);
            _prefabsParameters = AssetDatabase.LoadAssetAtPath<HoleGeneratorPrefabsParameters>(path);
        }
    }
}