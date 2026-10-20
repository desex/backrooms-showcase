using Db.Graphics;
using Ecs.Core;
using Ecs.Core.Interfaces;
using Ecs.Utils;
using Game.Services.Performance;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;

namespace Ecs.Game.Systems.Performance
{
    [Install(ExecutionType.Game, ExecutionPriority.Urgent, -100, nameof(EFeatures.Performance))]
    public sealed class AdaptivePerformanceTickSystem : AFixedTimeUpdateSystem
    {
        private readonly IAdaptivePerformanceService _adaptivePerformanceService;
        private readonly IAdaptivePerformanceSettingsBase _adaptivePerformanceSettings;

        protected override float FixedUpdateTime => _adaptivePerformanceSettings.TimeBetweenChecks;

        public AdaptivePerformanceTickSystem(
            ITimeProvider timeProvider,
            IAdaptivePerformanceSettingsBase adaptivePerformanceSettings,
            IAdaptivePerformanceService adaptivePerformanceService
        ) : base(timeProvider)
        {
            _adaptivePerformanceSettings = adaptivePerformanceSettings;
            _adaptivePerformanceService = adaptivePerformanceService;
        }

        protected override void Execute()
        {
#if !UNITY_EDITOR
            if (!_adaptivePerformanceSettings.IsAdaptivePerformanceEnabled)
                return;

            if (!_adaptivePerformanceService.IsGpuTimeLowerThanNeeded)
                return;

            _adaptivePerformanceService.TryDecreaseGraphicsSettings();
#endif
        }
    }
}