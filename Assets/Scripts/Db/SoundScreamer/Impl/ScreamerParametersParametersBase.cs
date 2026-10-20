using System.Collections.Generic;
using UnityEngine;

namespace Db.SoundScreamer.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(ScreamerParametersParametersBase), fileName = nameof(ScreamerParametersParametersBase))]
    public class ScreamerParametersParametersBase : ScriptableObject, IScreamerParametersBase
    {
        [SerializeField] private List<ScreamerParameterVo> _screamerFxVos;
        
        public IReadOnlyList<ScreamerParameterVo> AllScreamerParameters => _screamerFxVos;
    }
}