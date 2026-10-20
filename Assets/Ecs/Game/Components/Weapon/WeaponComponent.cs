using Db.Weapons;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Weapon
{
    [Game]
    public class WeaponComponent : IComponent
    {
        public WeaponVo Value;
    }
}