using System;
using System.Collections.Generic;
using Game.Utils;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Db.SceneLoading.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(ScenesBase), fileName = nameof(ScenesBase))]
    public class ScenesBase : SerializedScriptableObject, IScenesBase
    {
        [OdinSerialize] private Dictionary<EGameLevelType, SceneRef> _sceneRefs;

        private void OnValidate()
        {
            // Более показать не могу
        }

        public string GetSceneNameByType(EGameLevelType levelType)
        {
            // Более показать не могу
            return default;
        }
    }
}