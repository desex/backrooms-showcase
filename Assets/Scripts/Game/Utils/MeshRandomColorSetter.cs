using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Utils
{
    public class MeshRandomColorSetter : MonoBehaviour
    {
        private static readonly int _arrayIndexId = Shader.PropertyToID("_Array_Index");
        
        [SerializeField] private int _colorVariantsCount = 4;
        [SerializeField] private List<MeshRenderer> _meshRenderers;
        
        private MaterialPropertyBlock _currentPropertyBlock;

        public void RandomizeColor()
        {
            // Более показать не могу
        }

        public void AddMeshRenderers(MeshRenderer[] meshRenderers)
        {
            // Более показать не могу
        }

#if UNITY_EDITOR
        [Button("RandomizeColor")]
        private void RandomizeColorInEditor()
        {
            // Более показать не могу
        }
        
        [Button("Autofill Meshes")]
        private void AutofillMeshes()
        {
            // Более показать не могу
        }  
#endif
    }
}