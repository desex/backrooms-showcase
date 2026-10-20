using Game.Utils.Chunks;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Chunks
{
    [Game]
    public class WallTypeComponent : IComponent
    {
        public EChunkWallType Value;
    }
}