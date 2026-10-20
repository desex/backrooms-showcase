using System;

namespace Db.Tutorial.StateVos
{
    [Serializable]
    public struct TutorialSlidingParameters
    {
        public int CountOfSlides;
        public int CountOfSlidesWithSlowdown;
    }
}