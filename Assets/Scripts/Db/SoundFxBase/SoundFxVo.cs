using System;
using FMODUnity;

namespace Db.SoundFxBase
{
    [Serializable]
    public class SoundFxVo
    {
        public ESoundFxName SoundFxName;
        public ESoundType SoundType;
        public EventReference EventReference;
    }
}