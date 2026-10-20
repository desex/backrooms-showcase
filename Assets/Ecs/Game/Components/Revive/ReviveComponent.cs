using Ecs.Utils.Revive;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Revive
{
    [Game]
    public class ReviveComponent : IComponent
    {
        public ReviveData Value;
    }
}