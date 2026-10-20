using System;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Utils
{
    [Serializable]
    [HideReferenceObjectPicker]
    public class SceneRef
    {
#if UNITY_EDITOR
        [SerializeField] private UnityEditor.SceneAsset _sceneAsset;
#endif
        [SerializeField, HideInInspector] private string _sceneName;

        public string SceneName => _sceneName;

        public SceneRef()
        {

        }

        public SceneRef(string sceneName)
        {
            this._sceneName = sceneName;

#if UNITY_EDITOR
            _sceneAsset = null;
#endif
        }

        public static implicit operator SceneRef(string sceneName) => new SceneRef(sceneName);

        public void Init()
        {
#if UNITY_EDITOR
            if (_sceneAsset == default(Object))
            {
                _sceneName = null;
                return;
            }

            var path = UnityEditor.AssetDatabase.GetAssetPath(_sceneAsset);

            var name = path
                      .Split('/')
                      .Last()
                      .Split('.')
                      .First();

            _sceneName = name;
#endif
        }
    }
}