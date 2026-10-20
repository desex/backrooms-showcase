using Game.Utils.Tutorial;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Tutorial
{
    [Game]
    [Event(EventTarget.Self)]
    public class TutorialStateComponent : IComponent
    {
        public ETutorialState Value;
    }
}