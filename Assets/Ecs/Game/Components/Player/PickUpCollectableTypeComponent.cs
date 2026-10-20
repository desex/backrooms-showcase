using Ecs.Utils.ItemCollectables;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Player
{
    [Game]
    [Event(EventTarget.Self)]
    public class PickUpCollectableTypeComponent : IComponent
    {
        public ECollectableType Value;
    }
}