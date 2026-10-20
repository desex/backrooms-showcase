using System;
using UnityEngine;
using Utils.Drawers.Key;

namespace Db.Prefabs.Impls
{
    [CreateAssetMenu(menuName = "Settings/PrefabsBase", fileName = "PrefabsBase")]
    public class PrefabsBase : ScriptableObject, IPrefabsBase
    {
        [KeyValue(nameof(Prefab.name))] [SerializeField]
         private Prefab[] prefabs;

        public GameObject Get(string prefabName)
        {
            // Более показать не могу
            return default;
        }

        [Serializable]
        public class Prefab
        {
            public string name;
            public GameObject gameObject;
        }
    }
}