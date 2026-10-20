using System;
using System.Collections.Generic;
using UnityEngine;

namespace Db.Fade.Impl
{
    [CreateAssetMenu(menuName = "Settings/UI/" + nameof(FadeParametersBase), fileName = nameof(FadeParametersBase))]
    public class FadeParametersBase : ScriptableObject, IFadeParameters
    {
        
        [SerializeField] private List<FadeVo> _fades;
        
        public FadeVo GetFadeVo(EFadeType fadeType)
        {
            // Более показать не могу
            return default;
        }
    }
}