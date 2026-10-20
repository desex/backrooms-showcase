using System.Collections.Generic;
using Game.Utils.Performance;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Db.Graphics.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(AdaptivePerformanceSettingsBase), fileName = nameof(AdaptivePerformanceSettingsBase))]
    public sealed class AdaptivePerformanceSettingsBase : SerializedScriptableObject, IAdaptivePerformanceSettingsBase
    {
        [field: SerializeField] public bool IsAdaptivePerformanceEnabled { get; private set; }
        
        [field: EnableIf("IsAdaptivePerformanceEnabled")]
        [field: SerializeField] 
        public uint FpsTarget { get; private set; }
        
        [field: EnableIf("IsAdaptivePerformanceEnabled")]
        [field: SerializeField] 
        public float TimeBetweenChecks { get; private set; }

        [EnableIf("IsAdaptivePerformanceEnabled")]
        [SerializeField]
        private List<GraphicsDowngrade> _downGradesOrder;

        public IReadOnlyList<GraphicsDowngrade> DownGradesOrder => _downGradesOrder;
    }
}