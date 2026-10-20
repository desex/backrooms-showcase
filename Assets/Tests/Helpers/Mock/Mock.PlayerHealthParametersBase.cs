using Db.Player;
using UnityEngine;

namespace Tests.Helpers.Mock
{
    public static partial class Mock
    {
        public class PlayerHealthParametersBase : IPlayerHealthParametersBase
        {
            public float TimeBetweenInjuries { get; set; } = 5f;
            public float TimeBeforeRemoveDamageVisuals { get; } = 3f;
            public float TimeBetweenDamages { get; set; } = 0.25f;
            public int PlayerMaxHealth { get; set; } = 2;
            public float HitObstacleSideAngleThreshold { get; set; } = 45f;
            public Vector2 AngleRangeToPlayHitByObstacleAnimation { get; set; } = new Vector2(15, 95);
        }
    }
}