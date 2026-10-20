using System.Collections.Generic;

namespace Db.SoundFxBase
{
    public interface ISoundFxBase
    {
        List<SoundFxVo> AllSoundsList { get; }

        SoundFxVo GetSoundFxVoWithType(ESoundFxName soundName);
    }
}