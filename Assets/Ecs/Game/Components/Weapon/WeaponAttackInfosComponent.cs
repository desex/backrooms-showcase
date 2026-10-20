using System.Collections.Generic;
using Ecs.Utils;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Weapon
{
    [Game]
    public class WeaponAttackInfosComponent : IComponent
    {
        public List<AttackInfo> Value;
    }
}