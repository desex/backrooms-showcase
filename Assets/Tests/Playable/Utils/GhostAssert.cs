using System.Collections;
using Game.Utils.Ghost;
using Game.Utils.Movement;
using Tests.Playable.UITest.Custom.Conditions.Entity;

namespace Tests.Playable.Utils
{
    public static class GhostAssert
    {
        public static IEnumerator Alive(float timeout = 10f)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                        .SetConditionName($"Ghost is alive")
                        .SetEntityFilter(entity => entity.IsGhost && 
                                                   !entity.IsDead &&
                                                   !entity.IsDestroyed)
                        .SetCount(1)
                        .SetTimeout(timeout)
                        .Build();
        }
        
        public static IEnumerator Dead(float timeout = 10f)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                        .SetConditionName($"Ghost is dead")
                        .SetEntityFilter(entity => entity.IsGhost && 
                                                   entity.IsDead &&
                                                   !entity.IsDestroyed)
                        .SetCount(1)
                        .SetTimeout(timeout)
                        .Build();
        }
        
        public static IEnumerator IsDamaged(bool isDamaged, float timeout = 10f)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                        .SetConditionName($"Ghost is dead")
                        .SetEntityFilter(entity => entity.IsGhost && 
                                                   entity.IsDamaged == isDamaged &&
                                                   !entity.IsDestroyed)
                        .SetCount(1)
                        .SetTimeout(timeout)
                        .Build();
        }
        
        public static IEnumerator HasReadyForObstaclePass(bool hasReadyToObstaclePass, float timeout = 10f)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                        .SetConditionName($"Ghost has ReadyToObstaclePass")
                        .SetEntityFilter(entity => entity.IsGhost && 
                                                   entity.HasReadyToObstaclePass == hasReadyToObstaclePass &&
                                                   !entity.IsDestroyed)
                        .SetCount(1)
                        .SetTimeout(timeout)
                        .Build();
        }
        
        public static IEnumerator MadeDecision(EGhostSelectedActionResult actionResult, float timeout = 2f)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                        .SetConditionName($"Ghost make decision: {actionResult}")
                        .SetEntityFilter(entity => entity.IsGhost && 
                                                   entity.HasGhostChancedActionResult &&
                                                   entity.GhostChancedActionResult.Value == actionResult &&
                                                   !entity.IsDestroyed)
                        .SetCount(1)
                        .SetTimeout(timeout)
                        .Build();
        }
        
        public static IEnumerator MadeZoneMovementDecision(EGhostZoneMovementResult zoneMovementResult, float timeout = 10f)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                        .SetConditionName($"Ghost make zone movement decision: {zoneMovementResult}")
                        .SetEntityFilter(entity => entity.IsGhost && 
                                                   entity.HasGhostMovementWithZonesResult &&
                                                   entity.GhostMovementWithZonesResult.Value == zoneMovementResult &&
                                                   !entity.IsDestroyed)
                        .SetCount(1)
                        .SetTimeout(timeout)
                        .Build();
        }
        
        public static IEnumerator HasNeedJumpType(EGhostSimpleJumpType jumpType, float timeout = 10f)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                        .SetConditionName($"Ghost has {jumpType}")
                        .SetEntityFilter(entity => entity.IsGhost && 
                                                   entity.HasGhostNeedJumpType &&
                                                   entity.GhostNeedJumpType.Value == jumpType &&
                                                   !entity.IsDestroyed)
                        .SetCount(1)
                        .SetTimeout(timeout)
                        .Build();
        }
        
        public static IEnumerator NotHasNeedJump(float timeout = 10f)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                        .SetConditionName($"Ghost don't has need jump")
                        .SetEntityFilter(entity => entity.IsGhost && 
                                                   !entity.HasGhostNeedJumpType &&
                                                   !entity.IsDestroyed)
                        .SetCount(1)
                        .SetTimeout(timeout)
                        .Build();
        }
        
        public static IEnumerator IsGrounded(bool isGrounded, float timeout = 10f)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                        .SetConditionName($"Ghost is grounded: {isGrounded}")
                        .SetEntityFilter(entity => entity.IsGhost && 
                                                   entity.IsGrounded == isGrounded &&
                                                   !entity.IsDestroyed)
                        .SetCount(1)
                        .SetTimeout(timeout)
                        .Build();
        }
        
        public static IEnumerator HasJumping(EJumpType jumpType, float timeout = 10f)
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                        .SetConditionName($"Ghost has jump type: {jumpType}")
                        .SetEntityFilter(entity => entity.IsGhost && 
                                                   entity.HasJumping &&
                                                   entity.Jumping.Value == jumpType &&
                                                   !entity.IsDestroyed)
                        .SetCount(1)
                        .SetTimeout(timeout)
                        .Build();
        }
    }
}