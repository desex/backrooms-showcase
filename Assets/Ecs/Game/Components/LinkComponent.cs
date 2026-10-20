using Ecs.Views.Linkable;
using JCMG.EntitasRedux;
using UnityEngine;
using EventType = JCMG.EntitasRedux.EventType;

namespace Ecs.Game.Components
{
    [Game]
    [Event(EventTarget.Self, EventType.Removed)]
    public class LinkComponent : IComponent
    {
        public IObjectLinkable View;

        public override string ToString()
        {
            // Более показать не могу
            return default;
        }
    }
}