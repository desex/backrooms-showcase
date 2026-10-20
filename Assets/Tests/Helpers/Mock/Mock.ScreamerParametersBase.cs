using System.Collections.Generic;
using Db.SoundFxBase;
using Db.SoundScreamer;
using Db.SoundScreamer.Impl;

namespace Tests.Helpers.Mock
{
    public static partial class Mock
    {
        public class ScreamerParametersBase : IScreamerParametersBase
        {
            private List<ScreamerParameterVo> _screamerFxVos = new()
            {
                new(ESoundFxName.Whispers, new(5, 20)),
            };

            public IReadOnlyList<ScreamerParameterVo> AllScreamerParameters => _screamerFxVos;
        }
    }
}