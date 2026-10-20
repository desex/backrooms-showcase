using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Core.Utils
{
    [ExecuteInEditMode]
    public class MaterialTilingApplier : MonoBehaviour
    {
        [HideInInspector] public Vector2 Tiling = Vector2.one;
        private static readonly int TilingParameter = Shader.PropertyToID("_Tiling");
        private static readonly int TilingTextureParameter = Shader.PropertyToID("_BaseMap_ST");
        
        [Button]
        public void SetTilingButton()
        {
            var tiling = gameObject.transform.localScale;
            SetTiling(new Vector2(tiling.x, tiling.z));
            ApplyTiling();
        }

        public void SetTiling(Vector2 tiling)
        {
            if(tiling == default) 
                return;
        
            Tiling = tiling;
        }

        private void OnEnable()
        {
            ApplyTiling();
        }

        private void ApplyTiling()
        {
            if (gameObject == null) return;

            var objectRenderer = gameObject.GetComponentInChildren<Renderer>();
            if (objectRenderer == null) return;

            var propertyBlock = new MaterialPropertyBlock();
            objectRenderer.GetPropertyBlock(propertyBlock);
    
            propertyBlock.SetVector(TilingParameter, new Vector4(Tiling.x, Tiling.y, 0, 0));
            propertyBlock.SetVector(TilingTextureParameter, new Vector2(Tiling.x, Tiling.y));
    
            objectRenderer.SetPropertyBlock(propertyBlock);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (!EditorApplication.isPlaying)
            {
                ApplyTiling();
                EditorUtility.SetDirty(this);
            }
        }
#endif
    }
}
