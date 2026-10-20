using System;
using System.Collections.Generic;
using System.Linq;
using Db.GameLevelsSettings.Impls.Core.GenerationSettings;
using Db.GameLevelsSettings.Impls.Obstacles;
using Game.Utils.Obstacles;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Editor.Helpers
{
    public class MissingLinksCheckerEditor
    {
        [MenuItem("Tools/Check links in configs")]
        public static void CheckLinksInConfig()
        {
            // Более показать не могу
        }

        private static void CheckLinksInObstacleBases(string searchPath)
        {
            // Более показать не могу
        }
        
        private static void CheckLinksInChunkGenerationSettings(string searchPath)
        {
            // Более показать не могу
        }
        
        private static void AssertIsCollectionCorrect<T>(IEnumerable<T> collectionToCheck, string exceptionBeginning)
        {
            // Более показать не могу
        }

        private static void AssertNotNull(object objectToCheck, string errorMessage)
        {
            // Более показать не могу
        }
        
        private static void AssertIsExisting(Object objectToCheck, string errorMessage)
        {
            // Более показать не могу
        }
    }
}