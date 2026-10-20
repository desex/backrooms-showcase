using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Render.Utils
{
    [Serializable]
    public sealed class TvDistortionSettings
    {
        [Range(0f, 1f)] public float ShaderStrength = 1f;
        
        [MinValue(1)] public int LinesCount = 50;
        [Range(0f, 1f)] public float LinesSpeed = 0.09f;
        public Color LinesColor = new Color32(58, 58, 58, 140);
        [MinValue(0f)] public float LinesColorPower = 0.6f;
        
        [SerializeField]
        [MinMaxSlider(0f, 1f, true)] 
        private Vector2 _linesSizeBorders = new Vector2(0.2f, 0.8f);
        
        [MinValue(0f)] public float DistortionSpeed = 0.5f;
        [Range(0f, 1f)] public float DistortionStrength = 0.002f;
        [MinValue(0f)] public float DistortionNoiseScale = 15f;
        
        public Vector4 LinesSizeBorders => new Vector4(_linesSizeBorders.x, _linesSizeBorders.y, 0, 0);
    }
}