namespace Db.Render.SpeedShader
{
    public interface IBorderedSpeedShaderParameters
    {
        public float ShaderActivationSpeed { get; }
        public float InitialSpeed { get; }
        public float TargetAllSideSpeed { get; }
        public float TargetBorderSideSpeed { get; }
        public float InitialMaskSize { get; }
        public float TargetAllSideMaskSize { get; }
        public float TargetBorderSideMaskSize { get; }
        public float InitialMaskContrast { get; }
        public float TargetAllSideMaskContrast { get; }
        public float TargetBorderSideMaskContrast { get; }
        public float InitialSmoothness { get; }
        public float TargetAllSideSmoothness { get; }
        public float TargetBorderSideSmoothness { get; }
        public float InitialPower { get; }
        public float TargetAllSidePower { get; }
        public float TargetBorderSidePower { get; }
    }
}