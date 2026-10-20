using Ecs.Managers;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components
{
    [Game]
    public class OwnerComponent : IComponent
    {
        [EntityIndex]
        public Uid Uid;
    }
}