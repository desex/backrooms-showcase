using Sirenix.OdinInspector;
using UnityEngine;

namespace Db.Render.SpeedShader.Impl
{
    [CreateAssetMenu(menuName = "Settings/Renderer/" + nameof(BorderedSpeedShaderParameters), fileName = nameof(BorderedSpeedShaderParameters))]
    public class BorderedSpeedShaderParameters : ScriptableObject, IBorderedSpeedShaderParameters
    {
        [field: SerializeField] public float ShaderActivationSpeed { get; private set; }

        [field: FoldoutGroup("Speed")]
        [field: SerializeField] 
        public float InitialSpeed { get; private set; }
        [field: FoldoutGroup("Speed")]
        [field: SerializeField] 
        public float TargetAllSideSpeed { get; private set; }
        [field: FoldoutGroup("Speed")]
        [field: SerializeField] 
        public float TargetBorderSideSpeed { get; private set; }

        [field: FoldoutGroup("MaskSize")]
        [field: SerializeField] 
        public float InitialMaskSize { get; private set; }
        [field: FoldoutGroup("MaskSize")]
        [field: SerializeField] 
        public float TargetAllSideMaskSize { get; private set; }
        [field: FoldoutGroup("MaskSize")]
        [field: SerializeField] 
        public float TargetBorderSideMaskSize { get; private set; }

        [field: FoldoutGroup("MaskContrast")]
        [field: SerializeField] 
        public float InitialMaskContrast { get; private set; }
        [field: FoldoutGroup("MaskContrast")]
        [field: SerializeField] 
        public float TargetAllSideMaskContrast { get; private set; }
        [field: FoldoutGroup("MaskContrast")]
        [field: SerializeField] 
        public float TargetBorderSideMaskContrast { get; private set; }

        [field: FoldoutGroup("Smoothness")]
        [field: SerializeField] 
        public float InitialSmoothness { get; private set; }
        [field: FoldoutGroup("Smoothness")]
        [field: SerializeField] 
        public float TargetAllSideSmoothness { get; private set; }
        [field: FoldoutGroup("Smoothness")]
        [field: SerializeField] 
        public float TargetBorderSideSmoothness { get; private set; }

        [field: FoldoutGroup("Power")]
        [field: SerializeField] 
        public float InitialPower { get; private set; }
        [field: FoldoutGroup("Power")]
        [field: SerializeField] 
        public float TargetAllSidePower { get; private set; }
        [field: FoldoutGroup("Power")]
        [field: SerializeField] 
        public float TargetBorderSidePower { get; private set; }
    }
}