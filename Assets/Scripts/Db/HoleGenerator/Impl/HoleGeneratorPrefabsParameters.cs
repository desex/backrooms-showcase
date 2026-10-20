using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Db.HoleGenerator.Impl
{
    [CreateAssetMenu(
        menuName = "Settings/HoleGenerator/" + nameof(HoleGeneratorPrefabsParameters),
        fileName = nameof(HoleGeneratorPrefabsParameters)
    )]
    public class HoleGeneratorPrefabsParameters : SerializedScriptableObject, IHoleGeneratorPrefabsParameters
    {
        [SerializeField]
        private Dictionary<EGeneratorPrefab, HoleGeneratorPrefabVo[]> _prefabsCollections;

        public HoleGeneratorPrefabVo[] GetPrefab(EGeneratorPrefab prefabType)
        {
            if (_prefabsCollections.TryGetValue(prefabType, out var generatorPrefabVos))
            {
                return generatorPrefabVos;
            }

            Debug.LogError($"[{nameof(HoleGeneratorPrefabsParameters)}]: Failed to get prefab by type: {prefabType}");
            return null;
        }
    }
}