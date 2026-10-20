using System;

namespace Db.Tutorial.StateVos
{
    [Serializable]
    public struct TutorialDashParameters
    {
        public float PickCollectablesHintShowDistance;
        public float DashThroughColumnHintShowDistance;
        public float SecondDashThroughColumnHintShowDistance;
        public float DotCheckThresholdForwardDistance;
        public int TotalWatersCount;
    }
}