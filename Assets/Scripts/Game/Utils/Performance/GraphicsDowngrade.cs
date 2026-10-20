using System;
using Sirenix.OdinInspector;

namespace Game.Utils.Performance
{
    [Serializable]
    public struct GraphicsDowngrade
    {
        public EAdaptivePerformanceAction Action;
        
        [ShowIf("@this.Action == EAdaptivePerformanceAction.Decrease || "
              + "this.Action == EAdaptivePerformanceAction.Increase")]
        public EGraphicsSmoothDowngradeType SmoothDowngradeType;

        [ShowIf("@this.Action == EAdaptivePerformanceAction.ToggleEnable || "
              + "this.Action == EAdaptivePerformanceAction.ToggleDisable")]
        public EGraphicsDiscreteDowngradeType DiscreteDowngradeType;
        
        [ShowIf("@this.Action == EAdaptivePerformanceAction.Increase &&"
              + "this.SmoothDowngradeType != EGraphicsSmoothDowngradeType.None")]
        public float IncreaseTarget;
        
        [ShowIf("@this.Action == EAdaptivePerformanceAction.Decrease &&"
              + "this.SmoothDowngradeType != EGraphicsSmoothDowngradeType.None")]
        public float DecreaseTarget;

        [ShowIf("@(this.Action == EAdaptivePerformanceAction.Decrease || "
              + "this.Action == EAdaptivePerformanceAction.Increase) &&"
              + "this.SmoothDowngradeType != EGraphicsSmoothDowngradeType.None")]
        public float Step;
    }
}