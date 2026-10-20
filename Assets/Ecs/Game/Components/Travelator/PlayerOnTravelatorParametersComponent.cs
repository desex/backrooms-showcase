using Ecs.Utils.Travelator;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Travelator
{
    [Game]
    [Event(EventTarget.Self)]
    public class PlayerOnTravelatorParametersComponent : IComponent
    {
        public TravelatorData Value;
    }
}