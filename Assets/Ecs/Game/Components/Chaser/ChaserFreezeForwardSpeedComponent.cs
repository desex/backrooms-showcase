using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Chaser
{
    [Game]
    [Event(EventTarget.Self)]
    public class ChaserFreezeForwardSpeedComponent : IComponent
    {
        public float Value;
    }
}