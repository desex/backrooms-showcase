using System.Collections.Generic;
using Ecs.Commands.Generator;
using Game.Utils.Lights;

namespace Ecs.Action.Commands.Lights
{
    [Command]
    public struct PlayLightEventCommand
    {
        public List<ELightEvent> LightEvents;
    }
}