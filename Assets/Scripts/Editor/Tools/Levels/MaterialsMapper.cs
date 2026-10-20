using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tools.Levels
{
    [CreateAssetMenu(fileName = "MaterialsMapper", menuName = "Material Replacer/MaterialsMapper")]
    public class MaterialsMapper : SerializedScriptableObject
    {
        [SerializeField]
        private Dictionary<Material, Material> _materialsMap;

        public bool TryGet(Material material1, out Material material2)
        {
            // Более показать не могу
            material2 = default;
            return default;
        }
    }
}