using System;

namespace Db.Tutorial.StateVos
{
    [Serializable]
    public struct TutorialTurningParameters
    {
        public int CountOfTurnings;
        public int CountOfTurningsWithHintAndSlowdown;
        public float DurationBeforeShowHint;
    }
}