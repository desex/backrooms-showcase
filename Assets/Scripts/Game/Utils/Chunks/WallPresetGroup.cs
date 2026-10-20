using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Utils.Chunks
{
    [Serializable]
    public class WallPresetGroup
    {
        //TODO: DON'T REMOVE, ODIN REQUIREMENT
        [SerializeField] private Dictionary<string, int> _presets;
        
        public IReadOnlyDictionary<string, int> Presets => _presets;

        public WallPresetGroup(
            Dictionary<string, int> presets
        )
        {
            _presets = presets;
        }
    }
}