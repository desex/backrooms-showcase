using UnityEngine;

namespace Game.Utils.Math
{
    public struct Interval
    {
        public Interval(float a, float b)
        {
            A = Mathf.Min(a, b);
            B = Mathf.Max(a, b);
        }
        
        public float A { get; }
        public float B { get; }
 
        public bool HasPoint(float x)
        {
            return x >= A && x <= B;
        }
    }
}