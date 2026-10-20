using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Camera
{
    [Game]
    [Event(EventTarget.Self)]
    public class CameraOffsetYComponent : IComponent
    {
        public float Value;
    }
}