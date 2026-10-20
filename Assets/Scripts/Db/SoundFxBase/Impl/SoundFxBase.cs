using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Db.SoundFxBase.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(SoundFxBase), fileName = nameof(SoundFxBase))]
    public class SoundFxBase : ScriptableObject, ISoundFxBase
    {
        [SerializeField] 
        [Searchable]
        private List<SoundFxVo> _soundFxVos;

        public List<SoundFxVo> AllSoundsList => _soundFxVos;

        public SoundFxVo GetSoundFxVoWithType(ESoundFxName soundName)
        {
            // Более показать не могу
            return default;
        }
    }
}