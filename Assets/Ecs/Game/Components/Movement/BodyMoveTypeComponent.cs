using Game.Utils.Movement;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Movement
{
    [Game]
    [Event(EventTarget.Self)]
    public class BodyMoveTypeComponent : IComponent
    {
        public EBodyMoveType Value;
    }
}