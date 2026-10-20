using UnityEngine.Splines;

namespace Game.Utils.Chunks
{
    public readonly struct Path
    {
        public readonly SplineContainer Spline;
        public readonly float Length;
        public readonly float LengthPassed;
        
        public Path(SplineContainer spline, float length, float lengthPassed)
        {
            Spline = spline;
            Length = length;
            LengthPassed = lengthPassed;
        }
    }
}