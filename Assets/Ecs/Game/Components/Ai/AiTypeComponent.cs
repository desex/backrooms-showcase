using Db.Ai;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Ai
{
    [Game]
    public class AiTypeComponent : IComponent
    {
        public EAiType Value;
    }
}