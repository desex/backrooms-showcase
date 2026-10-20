using UnityEngine;

namespace Game.Utils.MathfExtension
{
    public static class MathfExtension
    {
        public static float Round(float roundValue, int roundNumber)
        {
            return Mathf.Round(roundValue * 10 * roundNumber) / (10f * roundNumber);
        }

        public static Vector3 Round(Vector3 vector3, int roundNumber)
        {
            return new(
                Round(vector3.x, roundNumber),
                Round(vector3.y, roundNumber),
                Round(vector3.z, roundNumber)
            );
        }

        public static float SafeDivide(float numerator, float denominator)
        {
            return denominator == 0f ? 0f : numerator / denominator;
        }
    }
}