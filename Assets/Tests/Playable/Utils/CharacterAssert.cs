using System.Collections;
using Tests.Playable.UITest.Custom.Conditions.Entity;

namespace Tests.Playable.Utils
{
    public static class CharacterAssert
    {
        public static IEnumerator ReadyToTurn()
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                .SetConditionName("Ready to turn")
                .SetEntityFilter(entity => entity.IsPlayer && entity.IsReadyToTurn && !entity.IsDestroyed)
                .SetCount(1)
                .SetTimeout(10)
                .Build();
        }
        
        public static IEnumerator NotReadyToTurn()
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                        .SetConditionName("Is not ready to turn")
                        .SetEntityFilter(entity => entity.IsPlayer && !entity.IsReadyToTurn && !entity.IsDestroyed)
                        .SetCount(1)
                        .SetTimeout(10)
                        .Build();
        }

        public static IEnumerator HasTurning()
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                .SetConditionName("Turning")
                .SetEntityFilter(entity => entity.IsPlayer && entity.HasTurning && !entity.IsDestroyed)
                .SetCount(1)
                .SetTimeout(10)
                .Build();
        }

        public static IEnumerator NotTurning()
        {
            yield return new EntityMatchCase<GameEntity>.Builder()
                .SetConditionName("Turning")
                .SetEntityFilter(entity => entity.IsPlayer && !entity.HasTurning && !entity.IsDestroyed)
                .SetCount(1)
                .SetTimeout(10)
                .Build();
        }
    }
}