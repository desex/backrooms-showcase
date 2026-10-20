using Game.Utils.Movement;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Player
{
    [Game]
    public class ChunkCenterComponent : IComponent
    {
        public ChunkCenterParameters Value;
    }
}