using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Db.SoundFxBase
{
    [Serializable]
    public struct SoundFxParameter
    {
        public ESoundParameter Type;
        public List<int> Keys;
        
        [HideIf(nameof(Name), nameof(ESoundParameter.Null))]
        public string Name { get;private set; }

        public SoundFxParameter(SoundFxParameter soundParameter)
        {
            Type = soundParameter.Type;
            Name = soundParameter.Type.ToString();
            Keys = soundParameter.Keys;
        }
    }
}