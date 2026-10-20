using System.Collections.Generic;
using Game.Utils.Performance;

namespace Db.Graphics
{
    public interface IAdaptivePerformanceSettingsBase
    {
        bool IsAdaptivePerformanceEnabled { get; }
        uint FpsTarget { get; }
        float TimeBetweenChecks { get; }
        IReadOnlyList<GraphicsDowngrade> DownGradesOrder { get; }
    }
}