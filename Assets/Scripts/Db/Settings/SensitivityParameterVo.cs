using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Db.Settings
{
    [Serializable]
    public class SensitivityParameterVo
    {
        public float DefaultValue;
        
        [SerializeField]
        [MinMaxSlider(0f, 3f, showFields: true)] 
        private Vector2 _value;
        
        public float MinValue => _value.x;
        public float MaxValue => _value.y;
    }
}