using Ecs.Utils;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Timer
{
    [Game]
    public class TimerTypeComponent : IComponent
    {
        public ETimerType Value;
    }
}