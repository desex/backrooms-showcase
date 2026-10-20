using System.Collections.Generic;

namespace Db.SoundScreamer
{
    public interface IScreamerParametersBase
    {
        IReadOnlyList<ScreamerParameterVo> AllScreamerParameters { get; }
    }
}